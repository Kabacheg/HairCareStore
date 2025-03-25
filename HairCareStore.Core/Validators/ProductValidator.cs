using FluentValidation;
using Hair_Care_Store.Core.Models;

namespace HairCareStore.Core.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator(){
            base.RuleFor((product) => product.Name)
            .NotEmpty().WithMessage("Name cannot be empty");

            base.RuleFor((product) => product.Description)
            .NotEmpty().WithMessage("Description cannot be empty");

            base.RuleFor((product) => product.Price)
            .NotEmpty().WithMessage("Price cannot be empty")
            .GreaterThan(0).WithMessage("Price should be more than 0");
            
        }
    }
}