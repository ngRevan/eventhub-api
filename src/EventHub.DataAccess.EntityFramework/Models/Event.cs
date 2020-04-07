using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public class Event : BaseEntity
    {
        public Event()
        {
            Members = new HashSet<EventMember>();
            Messages = new HashSet<Message>();
            Tasks = new HashSet<Task>();
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(600)]
        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public virtual ICollection<EventMember> Members { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
