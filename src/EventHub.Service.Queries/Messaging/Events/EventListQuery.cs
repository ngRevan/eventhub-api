using MediatR;
using System;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class EventListQuery : IRequest<EventListResult>
    {
        public string SearchText { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool? IsMember { get; set; }
    }
}
