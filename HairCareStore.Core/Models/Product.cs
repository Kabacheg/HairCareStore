using System.ComponentModel.DataAnnotations;

namespace Hair_Care_Store.Core.Models;
public class Product
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public decimal Price { get; set; }
}
