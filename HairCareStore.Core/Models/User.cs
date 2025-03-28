using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HairCareStore.Core.Models;
public class User
{
        public int Id { get; set; }

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

}