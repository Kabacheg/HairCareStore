using Hair_Care_Store.Models;
namespace Hair_Care_Store.Excpetions;
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

    public string CombineExceptionProperties(){
        string properties = "";
        foreach (var item in validationResponseItems)
        {
            properties += item.Property;
            properties += "|";
            
        }
        return properties.TrimEnd('|');
    }
}
