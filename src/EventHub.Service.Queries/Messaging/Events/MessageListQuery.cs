using MediatR;
using System;
using System.Text.Json.Serialization;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MessageListQuery : IRequest<MessageListResult>
    {
        [JsonIgnore]
        public Guid EventId { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
