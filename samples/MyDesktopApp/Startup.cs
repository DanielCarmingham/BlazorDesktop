using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Desktop;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyDesktopApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            StaticWebAssetsLoader.UseStaticWebAssets(null, null);

            var x = (IDesktopApplicationBuilder)app;
            x.AddComponent(typeof(App),"app");
        }
    }
}
