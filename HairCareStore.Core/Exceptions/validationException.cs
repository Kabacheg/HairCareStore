using Hair_Care_Store.Core.Models;
namespace Hair_Care_Store.Core.Excpetions;
public class ValidationException : Exception
{
    public List<ValidationResponse> validationResponseItems { get; set; }
    public ValidationException()
    {
        validationResponseItems  = new List<ValidationResponse>();
    }

    public string CombineExceptionMessages(){
        string message = "";
        foreach (var item in validationResponseItems)
        {
            message += item.Message;
            message += "|";
        }
        return message.TrimEnd('|');
    }

}
