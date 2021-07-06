using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Realdeal.Data.Models;

namespace Realdeal.Data
{
    public class RealdealDbContext : IdentityDbContext<ApplicationUser>
    {
        public RealdealDbContext(DbContextOptions<RealdealDbContext> options)
          : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Realdeal;Integrated Security=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {

            base.OnModelCreating(mb);
        }
    }
}
