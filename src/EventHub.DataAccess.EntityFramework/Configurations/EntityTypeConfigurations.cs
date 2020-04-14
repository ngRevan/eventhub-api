using EventHub.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.DataAccess.EntityFramework.Configurations
{
    public static class EntityTypeConfigurations
    {
        public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>
            where TBase : BaseEntity
        {
            public virtual void Configure(EntityTypeBuilder<TBase> builder)
            {
                builder.HasOne(b => b.CreatedByUser).WithMany().HasForeignKey(b => b.CreatedByUserId).OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(b => b.ModifiedByUser).WithMany().HasForeignKey(b => b.ModifiedByUserId).OnDelete(DeleteBehavior.NoAction);
            }
        }

        public class EventConfiguration : BaseEntityConfiguration<Event>
        {
            public override void Configure(EntityTypeBuilder<Event> builder)
            {
                base.Configure(builder);
            }
        }

        public class EventMemberConfiguration : BaseEntityConfiguration<EventMember>
        {
            public override void Configure(EntityTypeBuilder<EventMember> builder)
            {
                base.Configure(builder);
                builder.HasIndex(em => new { em.EventId, em.UserId }).IsUnique();
                builder.HasOne(em => em.Event).WithMany(e => e.Members).HasForeignKey(em => em.EventId).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(em => em.User).WithMany(u => u.Events).HasForeignKey(em => em.UserId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class MessageConfiguration : BaseEntityConfiguration<Message>
        {
            public override void Configure(EntityTypeBuilder<Message> builder)
            {
                base.Configure(builder);
            }
        }
    }
}
