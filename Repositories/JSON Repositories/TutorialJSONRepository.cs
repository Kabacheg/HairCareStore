using System.Text.Json;
using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;
using Microsoft.IdentityModel.Tokens;

namespace Hair_Care_Store.Repositories;
public class TutorialJSONRepository : ITutorialsRepository
{
    private readonly string filePath = "C:\\Users\\User\\Desktop\\New folder\\Hair Care Store\\tutorials.json";
    private static int currentId = 1;

    public TutorialJSONRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public IEnumerable<Tutorial> GetAllTutorials()
    {
        var TutorialsIntoJSON = File.ReadAllText(filePath);
        if(TutorialsIntoJSON.IsNullOrEmpty())
            return new List<Tutorial>();
        var DeserializedTutorialsIntoJSON = JsonSerializer.Deserialize<List<Tutorial>>(TutorialsIntoJSON);
        return DeserializedTutorialsIntoJSON;
    }

    public void DeleteTutorial(int idToDelete)
    {
        var tutorials = GetAllTutorials().ToList();

        var tutorialToDelete = tutorials.First(t => t.Id == idToDelete);
        tutorials.Remove(tutorialToDelete);

        var SerializedTutorials = JsonSerializer.Serialize(tutorials);
        File.WriteAllText(filePath, SerializedTutorials);
    }

    public void AddTutorial(Tutorial tutorial)
    {
        var tutorials = new List<Tutorial>();
        if(GetAllTutorials() != null)
            tutorials = GetAllTutorials().ToList();
        tutorial.Id = currentId;
        currentId += 1;
        tutorials.Add(tutorial);
        var SerializedTutorials = JsonSerializer.Serialize(tutorials);
        File.WriteAllText(filePath, SerializedTutorials);
    }

    public void UpdateTutorial(Tutorial tutorial)
    {
        var tutorials = GetAllTutorials().ToList();

        var tutorialToUpdate = tutorials.FirstOrDefault(t => t.Id == tutorial.Id);
        if (tutorialToUpdate != null)
        {
            tutorialToUpdate.Topic = tutorial.Topic;
            tutorialToUpdate.Instruction = tutorial.Instruction;
        }
        else
            throw new Exception("No tutorial with such ID");

        var SerializedTutorials = JsonSerializer.Serialize(tutorials);
        File.WriteAllText(filePath, SerializedTutorials);
    }
}