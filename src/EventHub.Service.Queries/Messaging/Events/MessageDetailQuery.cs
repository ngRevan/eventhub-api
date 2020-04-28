using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MessageDetailQuery : IRequest<MessageDetailResult>
    {
        public Guid EventId { get; set; }

        public Guid Id { get; set; }
    }
}
