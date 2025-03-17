namespace Hair_Care_Store.Responses;

public class InternalServerErrorResponse
{

    public string Message { get; set; }

    public InternalServerErrorResponse(string message)
    {
        this.Message = message;
    }
}