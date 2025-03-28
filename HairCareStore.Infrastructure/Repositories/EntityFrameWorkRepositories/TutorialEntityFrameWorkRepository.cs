using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Infrastructure.Data;

namespace HairCareStore.Infrastructure.Repositories.EntityFrameWorkRepositories
{
    public class TutorialEntityFrameWorkRepository : ITutorialsRepository
    {
        private readonly HairCareStoreDbContext dbContext;

        public TutorialEntityFrameWorkRepository(HairCareStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void AddTutorial(Tutorial tutorial)
        {
            dbContext.Tutorials.AddAsync(tutorial);
            dbContext.SaveChanges();
        }

        public void DeleteTutorial(int idToDelete)
        {
            var tutorialToDelete = GetAllTutorials().FirstOrDefault(t => t.Id == idToDelete);
            if (tutorialToDelete != null)
                dbContext.Tutorials.Remove(tutorialToDelete);
            dbContext.SaveChanges();
        }

        public IEnumerable<Tutorial> GetAllTutorials()
        {
            var allTutorials = dbContext.Tutorials.ToList();
            return allTutorials;
        }

        public void UpdateTutorial(Tutorial tutorial)
        {
            dbContext.Tutorials.Update(tutorial);
            dbContext.SaveChanges();
        }

    }
}