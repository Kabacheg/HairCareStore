using HairCareStore.Core.Models;

namespace HairCareStore.Core.Repositories;
public interface IHttpLogRepository
{
    public Task InsertAsync(HttpLog log); 
}
