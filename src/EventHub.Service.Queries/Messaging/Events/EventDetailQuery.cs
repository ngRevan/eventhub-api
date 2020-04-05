using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class EventDetailQuery : IRequest<EventDetailResult>
    {
        public Guid Id { get; set; }
    }
}
