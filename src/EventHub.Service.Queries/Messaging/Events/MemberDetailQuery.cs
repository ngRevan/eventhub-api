using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MemberDetailQuery : IRequest<MemberDetailResult>
    {
        public Guid EventId { get; set; }

        public string UserId { get; set; }
    }
}
