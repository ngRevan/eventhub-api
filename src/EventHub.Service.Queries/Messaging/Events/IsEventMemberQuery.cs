using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class IsEventMemberQuery : IRequest<IsEventMemberResult>
    {
        public string UserId { get; set; }

        public Guid EventId { get; set; }
    }
}
