using API_MessageTrigger.Domain.Entities;
using FluentValidation;

namespace API_MessageTrigger.Service.Validators
{
    public class MessageTriggerValidator : AbstractValidator<MessageTrigger>
    {
        public MessageTriggerValidator()
        {
            RuleFor(c => c.InstanceName)
                .NotEmpty().WithMessage("Please enter the InstanceName.")
                .NotNull().WithMessage("Please enter the InstanceName.");

            RuleFor(c => c.Token)
               .NotEmpty().WithMessage("Please enter the Token.")
               .NotNull().WithMessage("Please enter the Token.");

            RuleFor(c => c.PhoneNumber)
              .NotEmpty().WithMessage("Please enter the PhoneNumber.")
              .NotNull().WithMessage("Please enter the PhoneNumber.");
        }
    }
}
