using System.Text.Json;
using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;
using Microsoft.IdentityModel.Tokens;

namespace Hair_Care_Store.Repositories;
public class ProductJSONRepository : IProductRepository
{
    private readonly string filePath = "C:\\Users\\User\\Desktop\\New folder\\Hair Care Store\\products.json";
    private static int currentId = 1;

    public ProductJSONRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public void AddProduct(Product product)
    {   
        var products = new List<Product>();
        if(GetAllProducts() != null)
            products = GetAllProducts().ToList();
        product.Id = currentId;
        currentId += 1;
        products.Add(product);
        var SerializedProducts = JsonSerializer.Serialize(products);
        File.WriteAllText(filePath, SerializedProducts);
    }

    public void DeleteProduct(int idToDelete)
    {
        var products = GetAllProducts().ToList();

        var productToDelete = products.First(p => p.Id == idToDelete);
        products.Remove(productToDelete);

        var SerializedProducts = JsonSerializer.Serialize(products);
        File.WriteAllText(filePath, SerializedProducts);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        var ProductsIntoJSON = File.ReadAllText(filePath);
        if(ProductsIntoJSON.IsNullOrEmpty())
            return new List<Product>();
        var DeserializedProductsIntoJSON = JsonSerializer.Deserialize<List<Product>>(ProductsIntoJSON);
        return DeserializedProductsIntoJSON;
    }

    public void UpdateProduct(Product product)
    {
        var products = GetAllProducts().ToList();

        var productToUpdate = products.FirstOrDefault(p => p.Id == product.Id);
        if (productToUpdate != null)
        {
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
        }
        else
            throw new Exception("No product with such ID");

        var SerializedProducts = JsonSerializer.Serialize(products);
        File.WriteAllText(filePath, SerializedProducts);
    }

}