using System;
using System.ComponentModel.DataAnnotations;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public virtual ApplicationUser ModifiedByUser { get; set; }
    }
}
