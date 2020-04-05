using System.ComponentModel.DataAnnotations;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public class Task : BaseEntity
    {
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
