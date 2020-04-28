using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class CreateEventCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
