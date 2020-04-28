using System;

namespace EventHub.Service.Queries.ViewModels
{
    public class MessageView
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
