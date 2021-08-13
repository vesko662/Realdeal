using CloudinaryDotNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Service.Advert;
using Realdeal.Service.Archive;
using Realdeal.Service.Category;
using Realdeal.Service.CloudinaryCloud;
using Realdeal.Service.Observe;
using Realdeal.Service.Report;
using Realdeal.Service.User;

namespace Realdeal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
          => Configuration = configuration;


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RealdealDbContext>(options
               => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RealdealDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            Account cloudinaryAccount = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);

            services.AddSingleton(cloudinary);

            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<ICategoryService, CategoryServise>();
            services.AddTransient<IArchiveService, ArchiveService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IObserveService, ObserveService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting();

            app.UseAuthentication()
               .UseAuthorization();

            app.UseEndpoints(endpoints
            =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
