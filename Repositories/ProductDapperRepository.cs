using Hair_Care_Store.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;
using Hair_Care_Store.Models;

namespace Hair_Care_Store.Repositories;
public class ProductDapperRepository : IProductRepository
{
   private static string connectionString = "Server=localhost;Database=ProductDataBase;Integrated Security=true;trustServerCertificate=true;";
   private SqlConnection connection = new SqlConnection(connectionString);

    public void AddProduct(Product product)
    {
        connection.Open();
        

        var affectedRowsCount = connection.Execute(@"INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)",
        param: new
                {   
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                });

    }

    public void DeleteProduct(int idToDelete)
    {
        connection.Open();
        var affectedRowsCount = connection.Execute(
            sql: @"delete from Products
                where Id = @Id",
            param: new
                {   
                    Id = idToDelete,
                });

    }

    public IEnumerable<Product> GetAllProducts()
    {
        connection.Open();
        var products = connection.Query<Product>("select * from Products");

        return products;
    }

    public void UpdateProduct(Product product)
    {
            connection.Open();

            var affectedRowsCount = connection.Execute(
                sql: @"update Products
                    set Name = @Name,
                    Description = @Description,
                    Price = @Price
                    where Id = @Id",
                param: new
                    {   
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price
                    });

    }
}
