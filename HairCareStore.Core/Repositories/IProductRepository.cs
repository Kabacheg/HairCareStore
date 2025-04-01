using Hair_Care_Store.Core.Models;
namespace Hair_Care_Store.Core.Repositories;
public interface IProductRepository
{
    public IEnumerable<Product> GetAllProducts();

    public void DeleteProduct(int idToDelete);

    public void AddProduct(Product product);

    public void UpdateProduct(Product product);
}
