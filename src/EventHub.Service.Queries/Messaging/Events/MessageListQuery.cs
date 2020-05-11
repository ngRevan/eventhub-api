using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MessageListQuery : IRequest<MessageListResult>
    {
        public Guid EventId { get; set; }
    }
}
