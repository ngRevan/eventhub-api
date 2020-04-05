using System;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public class Message : BaseEntity
    {
        public Guid EventId { get; set; }

        public string Text { get; set; }

        public virtual Event Event { get; set; }
    }
}
