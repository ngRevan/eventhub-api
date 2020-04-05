using EventHub.Service.Queries.ViewModels;
using System.Collections.Generic;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class EventListResult
    {
        public IEnumerable<EventView> Results { get; set; }
    }
}
