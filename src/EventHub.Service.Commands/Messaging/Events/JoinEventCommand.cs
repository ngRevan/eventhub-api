using MediatR;
using System;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class JoinEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
