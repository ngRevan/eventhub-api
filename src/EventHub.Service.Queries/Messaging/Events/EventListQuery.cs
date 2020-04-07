using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class EventListQuery : IRequest<EventListResult>
    {
        public string SearchText { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; }
    }
}
