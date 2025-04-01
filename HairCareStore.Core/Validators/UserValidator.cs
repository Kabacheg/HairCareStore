using FluentValidation;
using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Core.Models;

namespace HairCareStore.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IUserRepository userRepository){
            base.RuleFor((user) => user.Name)
            .NotEmpty().WithMessage("Name cannot be empty");

            base.RuleFor((user) => user.Surname)
            .NotEmpty().WithMessage("Surname cannot be empty");

            base.RuleFor((user) => user.Mail)
            .NotEmpty().WithMessage("Mail cannot be empty")
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(async (mail, cancellation) =>
            {
                var existingUsers = userRepository.GetAllUsers();
                return existingUsers.All(u => u.Mail != mail);
            }).WithMessage("This email is already in use.");;

            base.RuleFor((user) => user.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be more than 8 symbols");
            
        }
    }
}