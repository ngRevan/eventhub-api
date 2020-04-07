using EventHub.Service.Commands.Messaging.Events;
using FluentValidation;

namespace EventHub.Service.Commands.Validation.Events
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
            RuleFor(c => c.Description).MaximumLength(600);
            RuleFor(c => c.StartDateTime).NotEmpty();
            RuleFor(c => c.EndDateTime).NotEmpty().GreaterThan(c => c.StartDateTime);
        }
    }
}
