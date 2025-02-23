using Hair_Care_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hair_Care_Store.Repositories.Base;
public interface IProductRepository
{
    public IEnumerable<Product> GetAllProducts();

    public void DeleteProduct(int idToDelete);

    public void AddProduct(Product product);

    public void UpdateProduct(Product product);
}
