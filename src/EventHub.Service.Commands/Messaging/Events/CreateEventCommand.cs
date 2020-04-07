using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class CreateEventCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
