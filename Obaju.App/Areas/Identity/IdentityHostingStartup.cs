using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Obaju.App.Areas.Identity.IdentityHostingStartup))]
namespace Obaju.App.Areas.Identity
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