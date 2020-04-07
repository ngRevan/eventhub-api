using EventHub.DataAccess.EntityFramework.Configurations;
using EventHub.DataAccess.EntityFramework.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventHub.DataAccess.EntityFramework.DataContext
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EntityTypeConfigurations.EventConfiguration());
            builder.ApplyConfiguration(new EntityTypeConfigurations.EventMemberConfiguration());
            builder.ApplyConfiguration(new EntityTypeConfigurations.MessageConfiguration());
            builder.ApplyConfiguration(new EntityTypeConfigurations.TaskConfiguration());
        }
    }
}
