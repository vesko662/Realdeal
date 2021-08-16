using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Realdeal.Data;
using Realdeal.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Realdeal.Common.GlobalConstants;

namespace Realdeal.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepDatabaes(
           this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCategories(services);
            SeedRolesAndUsers(services);
            SeedAdverts(services);


            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var context = services.GetRequiredService<RealdealDbContext>();

            context.Database.Migrate();
        }
        private static void SeedCategories(IServiceProvider services)
        {
            var context = services.GetRequiredService<RealdealDbContext>();

            if (context.MainCategories.Any())
                return;

            var json = File.ReadAllText("./importCategories.json");

            var categories = JsonConvert.DeserializeObject<List<MainCategory>>(json);

            context.MainCategories.AddRange(categories);

            context.SaveChanges();
        }
        private static void SeedRolesAndUsers(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));

                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@admin.bg",
                        Firstname = "Admin",
                        Lastname = "Admin",
                    };

                    await userManager.CreateAsync(adminUser, "admin123");

                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }

                if (!await roleManager.RoleExistsAsync(userRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(userRole));

                    var user = new ApplicationUser
                    {
                        UserName = "user123",
                        Email = "user@user.bg",
                        Firstname = "George",
                        Lastname = "Ivanov",
                    };

                    await userManager.CreateAsync(user, "user123");

                    await userManager.AddToRoleAsync(user, userRole);
                }
            })
            .GetAwaiter()
            .GetResult();
        }
        private static void SeedAdverts(IServiceProvider services)
        {

            var context = services.GetRequiredService<RealdealDbContext>();

            if ( context.Adverts.Any())
                return;

            var descktopAdvert = new Advert()
            {
                Name = "Gaming computer MSI RTX 3060 i5-11400F 16GB RAM 480GB SSD",
                Description = "Components:" +
                              "Motherboard: MSI B560M PRO - E" +
                              "Processor: 6 core INTEL ROCKET LAKE CORE I5-11400F, 6 CORES, 2.60GHZ(UP TO 4.40GHZ), 12MB" +
                              "Cooler: Arctic Freezer 34 eSports Duo Gray" +
                              "RAM: Kingston DRAM 16GB 3200MHz DDR4 CL16 DIMM / 2 x 8GB / HyperX FURY RGB" +
                              "Video card: Gainward RTX 3060 Ghost 12GB GDDR6, 192bit PCI Express 4.0" +
                              "SSD: 480GB Kingston A400" +
                              "Power supply: AeroCool PSU LUX RGB 650W - Bronze, RGB Addressable -ACPB - LX65AEC.11" +
                              "Box: Makki Case ATX Gaming -F07 RGB 3F with 3 pcs.x 120 mm RGB fan" +
                              "Sold with Winodws 10 installed and all necessary drivers.",
                Price = 2630,
                SubCategory =  context.SubCategories.Where(x => x.Name == "Desktops").FirstOrDefault(),
                User =  context.Users.Where(x => x.UserName == "user123").FirstOrDefault(),
            };

            descktopAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629114916/advertImages/image_3_mhny4j.webp" });
            descktopAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629114916/advertImages/image_j6tc2p.webp" });
            descktopAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629114916/advertImages/image_2_geb0p7.webp" });
            descktopAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629114915/advertImages/image_1_wiebat.webp" });

             context.Adverts.Add(descktopAdvert);

            var furnitureAdvert = new Advert()
            {
                Name = "Meeting room table",
                Description = "Meeting room tavle. Brand new. Dimensions 1.30x4.20. Chipboard material, with holes for cables.",
                Price = 1400,
                SubCategory =  context.SubCategories.Where(x => x.Name == "Furniture").FirstOrDefault(),
                User =  context.Users.Where(x => x.UserName == "user123").FirstOrDefault(),
            };

            furnitureAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629115797/advertImages/Screenshot_4_swtur1.png" });
            furnitureAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629115797/advertImages/Screenshot_1_os0irl.png" });
            furnitureAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629115796/advertImages/Screenshot_2_ekt4i1.png" });
            furnitureAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629115796/advertImages/Screenshot_3_wkhyp4.png" });

             context.Adverts.Add(furnitureAdvert);

            var animalsAdvert = new Advert()
            {
                Name = "Russian pigeons",
                Description = "A young pair of reliable Russian pigeons",
                Price = 80,
                SubCategory =  context.SubCategories.Where(x => x.Name == "Birds").FirstOrDefault(),
                User =  context.Users.Where(x => x.UserName == "user123").FirstOrDefault(),
            };

            animalsAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116002/advertImages/Screenshot_5_usljoa.png" });
            animalsAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116002/advertImages/Screenshot_6_czjaod.png" });

             context.Adverts.Add(animalsAdvert);

            var autoAdvert = new Advert()
            {
                Name = "Mercedes-Benz S 500",
                Description = "Hello, the car is extremely well preserved. Imported from Germany by an official Mercedes dealership for Germany.",
                Price = 22985,
                SubCategory =  context.SubCategories.Where(x => x.Name == "Cars and SUVs").FirstOrDefault(),
                User =  context.Users.Where(x => x.UserName == "user123").FirstOrDefault(),
            };

            autoAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116237/advertImages/Screenshot_7_vxh59c.png" });
            autoAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116242/advertImages/Screenshot_8_bnfj7z.png" });
            autoAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116239/advertImages/Screenshot_9_jsbxmn.png" });
            autoAdvert.AdvertImages.Add(new AdvertImage() { ImageUrl = "https://res.cloudinary.com/dzlqshegm/image/upload/v1629116238/advertImages/Screenshot_10_keg7do.png" });

             context.Adverts.Add(autoAdvert);

             context.SaveChanges();
        }
    }

}
