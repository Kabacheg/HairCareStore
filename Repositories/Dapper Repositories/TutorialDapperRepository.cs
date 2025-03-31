using Hair_Care_Store.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;
using Hair_Care_Store.Models;

namespace Hair_Care_Store.Repositories;
public class TutorialDapperRepository : ITutorialsRepository
{
   private static string connectionString = "Server=localhost;Database=ProductDataBase;Integrated Security=true;trustServerCertificate=true;";
   private SqlConnection connection = new SqlConnection(connectionString);

    public void AddTutorial(Tutorial tutorial)
    {
        connection.Open();
        
        var affectedRowsCount = connection.Execute(@"INSERT INTO Tutorials (Topic, Instruction) VALUES (@Name, @Instruction)",
        param: new
                {   
                    Name = tutorial.Topic,
                    Instruction = tutorial.Instruction,
                });
        connection.Close();
    }

    public void DeleteTutorial(int idToDelete)
    {
        connection.Open();
        var affectedRowsCount = connection.Execute(
            sql: @"delete from Tutorials
                where Id = @Id",
            param: new
                {   
                    Id = idToDelete,
                });
        connection.Close();
    }

    public IEnumerable<Tutorial> GetAllTutorials()
    {
        connection.Open();
        var tutorials = connection.Query<Tutorial>("select * from Tutorials");
        connection.Close();
        return tutorials;
    }

    public void UpdateTutorial(Tutorial tutorial)
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

        connection.Close();
    }
}
