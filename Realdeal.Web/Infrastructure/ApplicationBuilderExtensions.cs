using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Realdeal.Data;
using Realdeal.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Realdeal.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> PrepDatabaes(
           this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

           await SeedCategories(services);
           //SeedUsersWithRoles(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<RealdealDbContext>();

            db.Database.Migrate();
        }
        private static async Task SeedCategories(IServiceProvider services)
        {
            var context = services.GetRequiredService<RealdealDbContext>();

            var json = File.ReadAllText("./importCategories.json");

            var categories = JsonConvert.DeserializeObject<List<MainCategory>>(json);

           await context.MainCategories.AddRangeAsync(categories);

            await context.SaveChangesAsync();
        }
    }
}
