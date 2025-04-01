using Microsoft.Data.SqlClient;
using Dapper;
using HairCareStore.Core.Models;
using HairCareStore.Infrastructure.Options;
using HairCareStore.Core.Repositories;
using Microsoft.Extensions.Options;

namespace HairCareStore.Infrastructure.Repositories.Dapper_Repositories;
public class HttpLogDapperRepository : IHttpLogRepository
{
    private readonly string connectionString;

    public HttpLogDapperRepository(IOptionsSnapshot<DatabaseOptions> options)
    {
        this.connectionString = options.Value.ConnectionString;
    }

    public async Task InsertAsync(HttpLog log)
    {
        using (var connection = new SqlConnection(this.connectionString))
        {
            await connection.OpenAsync();
    
            await connection.ExecuteAsync(
                sql: @"INSERT INTO HttpLog (RequestId, Url, RequestBody, RequestHeaders, MethodType, ResponseBody, ResponseHeaders, StatusCode, CreationDateTime, EndDateTime, ClientIp,  Message)
                       VALUES (@RequestId, @Url, @RequestBody, @RequestHeaders, @MethodType, @ResponseBody, @ResponseHeaders, @StatusCode, @CreationDateTime, @EndDateTime, @ClientIp, @Message);",
                param: log);
        }
    }


}