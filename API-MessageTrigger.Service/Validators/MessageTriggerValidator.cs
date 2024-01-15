using API_MessageTrigger.Domain.Entities;
using FluentValidation;

namespace API_MessageTrigger.Service.Validators
{
    public class MessageTriggerValidator : AbstractValidator<MessageTrigger>
    {
        public MessageTriggerValidator()
        {
            RuleFor(c => c.NameInstance)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Token)
               .NotEmpty().WithMessage("Please enter the name.")
               .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.PhoneNumber)
              .NotEmpty().WithMessage("Please enter the name.")
              .NotNull().WithMessage("Please enter the name.");
        }
    }
}
