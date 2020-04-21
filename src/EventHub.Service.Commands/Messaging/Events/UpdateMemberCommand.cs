using MediatR;
using System;
using System.Text.Json.Serialization;

namespace EventHub.Service.Commands.Messaging.Events
{
    public sealed class UpdateMemberCommand : IRequest
    {
        [JsonIgnore]
        public Guid EventId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        public bool IsAdmin { get; set; }
    }
}
