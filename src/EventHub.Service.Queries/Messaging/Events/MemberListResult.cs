using EventHub.Service.Queries.ViewModels;
using System.Collections.Generic;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MemberListResult
    {
        public IEnumerable<EventMemberView> Results { get; set; }
    }
}
