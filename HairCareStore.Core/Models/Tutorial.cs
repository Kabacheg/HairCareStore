using System.ComponentModel.DataAnnotations;

namespace Hair_Care_Store.Core.Models;
public class Tutorial
{
    public int Id { get; set; }

    [Required]
    public required string  Topic {get ; set;}

    [Required]
    public required string Instruction { get; set; }
}
