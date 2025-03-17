using System.Net;
using System.Text;
using Azure.Core;
using HairCareStore.Models;
using HairCareStore.Repositories.Base;
using HairCareStore.Services.Base;

namespace HairCareStore.Services;
public class HttpLogger : IHttpLogger
{
    private readonly IHttpLogRepository repository;

    public HttpLogger(IHttpLogRepository repository)
    {
        this.repository = repository;
    }

    public async Task LogAsync(HttpContext context, string? message = null)
    {

        var request = context.Request;
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer);
        var body = Encoding.UTF8.GetString(buffer);
        request.Body.Position = 0;
        // я попытался узнать у других как логгировать регвест и респос, не получилось
        
        var log = new HttpLog()
        {
            RequestId = context.TraceIdentifier,
            Url = context.Request.Path,
            RequestBody = body,
            RequestHeaders = context.Request.Headers.ToString(),
            MethodType = context.Request.Method,
            ResponseBody = context.Response.HasStarted ? null : "Response body logging not available",
            ResponseHeaders = context.Response.Headers.ToString(),
            StatusCode = (HttpStatusCode)context.Response.StatusCode,
            CreationDateTime = DateTime.UtcNow,
            EndDateTime = null,
            ClientIp = context.Connection.RemoteIpAddress?.ToString(),
            Message = message
        };


        
        context.Response.OnCompleted(async () =>
        {
            log.EndDateTime = DateTime.UtcNow;
            await this.repository.InsertAsync(log);
        });
    }

}


