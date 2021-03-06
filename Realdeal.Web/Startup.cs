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
using Realdeal.Service.EmailSender;
using Realdeal.Service.EmailSender.Configuration;
using Realdeal.Service.Message;
using Realdeal.Service.Observe;
using Realdeal.Service.Report;
using Realdeal.Service.User;
using Realdeal.Web.Hubs;
using Realdeal.Web.Infrastructure;

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

            services.AddSignalR();
            services.AddMemoryCache();

            Account cloudinaryAccount = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);

            services.AddSingleton(cloudinary);

            var emailConfiguration = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfiguration);

            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<ICategoryService, CategoryServise>();
            services.AddTransient<IArchiveService, ArchiveService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IObserveService, ObserveService>();
            services.AddTransient<IMessageService, MessageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            app.PrepDatabaes();


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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.UseEndpoints(endpoints
            =>
            {
                endpoints.MapHub<ChatHub>("/chat");

                endpoints.MapControllerRoute(
                name: "areaRoute",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
