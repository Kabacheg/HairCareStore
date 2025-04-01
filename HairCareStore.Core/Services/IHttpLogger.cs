using Microsoft.AspNetCore.Http;

namespace HairCareStore.Core.Services;
public interface IHttpLogger
{
    public Task LogAsync(HttpContext context, string? message = null);
}