using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MemberListQuery : IRequest<MemberListResult>
    {
        public Guid EventId { get; set; }
    }
}
