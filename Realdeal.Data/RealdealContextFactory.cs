using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Realdeal.Data
{
    public class RealdealContextFactory:IDesignTimeDbContextFactory<RealdealDbContext>
    {
        public RealdealDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RealdealDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=Realdeal;Integrated Security=true;");

            return new RealdealDbContext(optionsBuilder.Options);
        }
    }
}
