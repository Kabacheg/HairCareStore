using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Core.Models;
using HairCareStore.Infrastructure.Data;

namespace HairCareStore.Infrastructure.Repositories.EntityFrameWorkRepositories
{
    public class UserEntityFrameWorkRepository : IUserRepository
    {
        private readonly HairCareStoreDbContext dbContext;

        public UserEntityFrameWorkRepository(HairCareStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void AddUser(User user)
        {
            dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();
        }

        public void DeleteUser(int idToDelete)
        {
            var UserToDelete = GetAllUsers().FirstOrDefault(t => t.Id == idToDelete);
            if (UserToDelete != null)
                dbContext.Users.Remove(UserToDelete);
            dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var allUsers = dbContext.Users.ToList();
            return allUsers;
        }

        public void UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }

    }
}