using System.ComponentModel.DataAnnotations;

namespace HairCareStore.Core.Dtos
{
    public class UserDetailsForLoginDto
    {
        [Required]
        [EmailAddress]
        public required string Mail { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}