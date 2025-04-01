using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            var userRepository = validationContext.GetService<IUserRepository>();
            if (userRepository != null)
            {
                var existingUsers = userRepository.GetAllUsers();
                if (existingUsers.Any(u => u.Mail == email))
                {
                    return new ValidationResult("This email is already in use.");
                }
            }
        }

        return ValidationResult.Success;
    }
}
