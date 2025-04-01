using Hair_Care_Store.Core.Repositories;
using Microsoft.Data.SqlClient;
using Dapper;
using Hair_Care_Store.Core.Models;
using Microsoft.Extensions.Options;
using HairCareStore.Infrastructure.Options;

namespace Hair_Care_Store.Infrastructure.Repositories.Dapper_Repositories;
public class TutorialDapperRepository : ITutorialsRepository
{
   private readonly string connectionString;

    public TutorialDapperRepository(IOptionsSnapshot<DatabaseOptions> options)
    {
        this.connectionString = options.Value.ConnectionString;
    }

    public void AddTutorial(Tutorial tutorial)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var affectedRowsCount = connection.Execute(@"INSERT INTO Tutorials (Topic, Instruction) VALUES (@Name, @Instruction)",
            param: new
                    {   
                        Name = tutorial.Topic,
                        Instruction = tutorial.Instruction,
                    });
        }
    }

    public void DeleteTutorial(int idToDelete)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var affectedRowsCount = connection.Execute(
                sql: @"delete from Tutorials
                    where Id = @Id",
                param: new
                    {   
                        Id = idToDelete,
                    });
        }
    }

    public IEnumerable<Tutorial> GetAllTutorials()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var tutorials = connection.Query<Tutorial>("select * from Tutorials");
            return tutorials;
        }
    }

    public void UpdateTutorial(Tutorial tutorial)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var affectedRowsCount = connection.Execute(
                sql: @"update Tutorials
                    set Topic = @Topic,
                    Instruction = @Instruction
                    where Id = @Id",
                param: new
                    {   
                        Id = tutorial.Id,
                        Topic = tutorial.Topic,
                        Instruction = tutorial.Instruction,
                    });

        }
    }
}
