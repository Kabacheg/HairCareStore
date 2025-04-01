using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HairCareStore.Core.Dtos;
public class ProductDetailsForCreating
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public required IFormFile ProductPicture { get; set; }
}