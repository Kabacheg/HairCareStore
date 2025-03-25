namespace Hair_Care_Store.Core.Models;

public class ValidationResponse
{
    public string Message { get; set; }
    public ValidationResponse(string message)
    {
        this.Message = message;
    }
}