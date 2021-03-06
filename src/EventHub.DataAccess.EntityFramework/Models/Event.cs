﻿using System;
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
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(600)]
        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public virtual ICollection<EventMember> Members { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
