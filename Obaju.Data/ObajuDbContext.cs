using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Obaju.Models;

namespace Obaju.Data
{
    public class ObajuDbContext : IdentityDbContext<User>
    {
        public ObajuDbContext(DbContextOptions<ObajuDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
