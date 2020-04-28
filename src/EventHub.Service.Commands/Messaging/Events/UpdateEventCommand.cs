using MediatR;
using System;
using System.Text.Json.Serialization;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class UpdateEventCommand : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
