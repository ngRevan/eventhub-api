using EventHub.Service.Commands.Messaging.Events;
using FluentValidation;

namespace EventHub.Service.Commands.Validation.Events
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Text).NotEmpty().MaximumLength(1000);
        }
    }
}
