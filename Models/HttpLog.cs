namespace HairCareStore.Models;
using System.Net;

public class HttpLog
{
    public int Id { get; set; } = 0;
    public required string RequestId { get; set; }
    public required string Url { get; set; }
    public string? RequestBody { get; set; }
    public string? RequestHeaders { get; set; }
    public required string MethodType { get; set; }
    public string? ResponseBody { get; set; }
    public string? ResponseHeaders { get; set; }
    public required HttpStatusCode StatusCode { get; set; }
    public required DateTime CreationDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? ClientIp { get; set; }
    public string? Message { get; set; }

    public HttpLog(string requestId, string url, string methodType, HttpStatusCode statusCode, DateTime creationDateTime)
    {
        this.RequestId = requestId;
        this.Url = url;
        this.MethodType = methodType;
        this.StatusCode = statusCode;
        this.CreationDateTime = creationDateTime;
    }

    public HttpLog()
    {
        this.RequestId = string.Empty;
        this.Url = string.Empty;
        this.MethodType = string.Empty;
        this.StatusCode = HttpStatusCode.OK;
        this.CreationDateTime = DateTime.UtcNow;
    }
}