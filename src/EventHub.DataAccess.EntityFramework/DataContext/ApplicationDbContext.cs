using Microsoft.EntityFrameworkCore;

namespace EventHub.DataAccess.EntityFramework.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
