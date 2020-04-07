using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EventHub.DataAccess.EntityFramework.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            Events = new HashSet<EventMember>();
        }

        public virtual ICollection<EventMember> Events { get; set; }
    }
}
