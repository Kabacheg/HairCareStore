using FluentValidation;
using Hair_Care_Store.Core.Models;

namespace HairCareStore.Core.Validators
{
    public class TutorialValidator : AbstractValidator<Tutorial>
    {
        public TutorialValidator(){
            base.RuleFor((tutorial) => tutorial.Topic)
            .NotEmpty().WithMessage("Topic cannot be empty");

            base.RuleFor((tutorial) => tutorial.Instruction)
            .NotEmpty().WithMessage("Instruction cannot be empty");
        }
    }
}