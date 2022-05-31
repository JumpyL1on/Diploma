using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Diploma.WebAssembly;
using Diploma.WebAssembly.BusinessLogic.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddServices();

builder.Services.AddValidationServices();

builder.Services.AddScoped(serviceProvider =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7095/api/") };

    return httpClient.EnableIntercept(serviceProvider);
});

builder.Services.AddHttpClientInterceptor();

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();