using Hair_Care_Store.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;
using Hair_Care_Store.Models;
using Microsoft.Extensions.Options;
using HairCareStore.Options;

namespace Hair_Care_Store.Repositories;
public class ProductDapperRepository : IProductRepository
{
   private readonly string connectionString;

    public ProductDapperRepository(IOptionsSnapshot<DatabaseOptions> options)
    {
        this.connectionString = options.Value.ConnectionString;
    }

    public void AddProduct(Product product)
    {    
        using (var connection = new SqlConnection(connectionString))
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

    }

    public void DeleteProduct(int idToDelete)
    {
        using (var connection = new SqlConnection(connectionString))
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
    }

    public IEnumerable<Product> GetAllProducts()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var products = connection.Query<Product>("select * from Products");
            return products;
        }
    }   

    public void UpdateProduct(Product product)
    {
        using (var connection = new SqlConnection(connectionString))
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
}
