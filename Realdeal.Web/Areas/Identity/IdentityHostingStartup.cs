using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Realdeal.Web.Areas.Identity.IdentityHostingStartup))]
namespace Realdeal.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}