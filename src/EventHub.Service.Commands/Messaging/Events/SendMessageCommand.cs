using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class SendMessageCommand : IRequest
    {
        public Guid EventId { get; set; }

        public string Text { get; set; }
    }
}
