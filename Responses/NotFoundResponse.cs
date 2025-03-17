namespace Hair_Care_Store.Responses;

public class NotFoundResponse
{
    public string Message { get; set; }

    public NotFoundResponse(string message)
    {
        this.Message = message;
    }
}