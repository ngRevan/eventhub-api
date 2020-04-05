using System;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public class EventMember : BaseEntity
    {
        public Guid EventId { get; set; }

        public string UserId { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Event Event { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
