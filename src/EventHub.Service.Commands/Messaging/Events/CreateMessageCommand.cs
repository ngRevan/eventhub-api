using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class CreateMessageCommand : IRequest
    {
        public Guid EventId { get; set; }

        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
