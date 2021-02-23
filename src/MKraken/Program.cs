using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKraken.Domain;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MKraken
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return config.GetSection("AppConfig").Get<AppConfig>();
            });
        }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(Program))
                .AddClasses().AsImplementedInterfaces().WithTransientLifetime());

            await builder.Build().RunAsync();
        }
    }
}