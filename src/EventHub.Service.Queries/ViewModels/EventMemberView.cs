using System;

namespace EventHub.Service.Queries.ViewModels
{
    public class EventMemberView
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsAdmin { get; set; }
    }
}
