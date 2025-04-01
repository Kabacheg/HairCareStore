using Hair_Care_Store.Core.Models;
using HairCareStore.Core.Models;
namespace Hair_Care_Store.Core.Repositories;
public interface IUserRepository
{
    public IEnumerable<User> GetAllUsers();

    public void DeleteUser(int idToDelete);

    public void AddUser(User user);

    public void UpdateUser(User user);
}
