using Microsoft.EntityFrameworkCore;
using Realdeal.Data;
using System;

namespace Realdeal.Test.Service
{
   public class TestHelper
    {
        public  RealdealDbContext CreateDbInMemory()
        {
            var options = new DbContextOptionsBuilder<RealdealDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            return new RealdealDbContext(options.Options);
        }
    }
}
