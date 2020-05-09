using MessagePack;
using System;

namespace EventHub.Service.Queries.ViewModels
{
    [MessagePackObject]
    public class MessageView
    {
        [Key("id")]
        public Guid Id { get; set; }

        [Key("eventId")]
        public Guid EventId { get; set; }

        [Key("text")]
        public string Text { get; set; }

        [Key("username")]
        public string Username { get; set; }

        [Key("createdByUserId")]
        public string CreatedByUserId { get; set; }

        [Key("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
