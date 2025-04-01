using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Infrastructure.Data;

namespace HairCareStore.Infrastructure.Repositories.EntityFrameWorkRepositories;

public class ProductEntityFrameWorkRepository : IProductRepository
{
    private readonly HairCareStoreDbContext dbContext;

    public ProductEntityFrameWorkRepository(HairCareStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void AddProduct(Product product)
    {
        dbContext.Products.AddAsync(product);
        dbContext.SaveChanges();
    }

    public void DeleteProduct(int idToDelete)
    {
        var productToDelete = GetAllProducts().FirstOrDefault(p => p.Id == idToDelete);
        if (productToDelete != null)
            dbContext.Products.Remove(productToDelete);
        dbContext.SaveChanges();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        var allProducts = dbContext.Products.ToList();
        return allProducts;
    }
    
    public void UpdateProduct(Product product)
    {
        dbContext.Products.Update(product);
        dbContext.SaveChanges();
    }
}
