using HairCareStore.Models;

namespace HairCareStore.Repositories.Base;
public interface IHttpLogRepository
{
    public Task InsertAsync(HttpLog log); 
}
