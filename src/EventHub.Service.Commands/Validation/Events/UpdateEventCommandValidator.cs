﻿using EventHub.Service.Commands.Messaging.Events;
using FluentValidation;

namespace EventHub.Service.Commands.Validation.Events
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
            RuleFor(c => c.Description).MaximumLength(600);
            RuleFor(c => c.StartDate.Date).NotEmpty().WithName("Start date");
            RuleFor(c => c.EndDate.Date).NotEmpty().GreaterThanOrEqualTo(c => c.StartDate.Date).WithName("End date");
        }
    }
}
