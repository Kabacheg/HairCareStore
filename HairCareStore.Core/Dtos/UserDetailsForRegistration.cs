using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HairCareStore.Core.Dtos;

public class UserDetailsForRegistration
{
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Surname { get; set; }

        [Required]
        [EmailAddress]
        [UniqueEmail]
        public required string Mail { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }

        [Required]
        public required IFormFile UserAvatar { get; set; }

}