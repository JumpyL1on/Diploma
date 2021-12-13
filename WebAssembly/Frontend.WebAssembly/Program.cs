using System.Threading.Tasks;
using Frontend.Application.Extensions;
using Frontend.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Frontend.WebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddMudServices();
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("Owner", policyBuilder => policyBuilder.RequireClaim("Role", "Owner"));
                options.AddPolicy("Participant", policyBuilder => policyBuilder.RequireClaim("Role", "Participant"));
            });
            await builder.Build().RunAsync();
        }
    }
}