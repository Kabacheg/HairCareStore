using Hair_Care_Store.Models;
namespace Hair_Care_Store.Excpetions;
public class ValidationException : Exception
{
    public List<ValidationResponse> validationResponseItems { get; set; }
    public ValidationException()
    {
        validationResponseItems  = new List<ValidationResponse>();
    }
}
