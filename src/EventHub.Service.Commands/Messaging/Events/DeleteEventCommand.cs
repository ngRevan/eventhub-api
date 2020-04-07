using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class DeleteEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
