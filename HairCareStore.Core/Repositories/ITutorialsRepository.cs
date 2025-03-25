using Hair_Care_Store.Core.Models;

namespace Hair_Care_Store.Core.Repositories;
public interface ITutorialsRepository
{
    public IEnumerable<Tutorial> GetAllTutorials();

    public void DeleteTutorial(int idToDelete);

    public void AddTutorial(Tutorial tutorial);

    public void UpdateTutorial(Tutorial tutorial);
}
