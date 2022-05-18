using Blazored.LocalStorage;
using Diploma.Common.Interfaces;
using Diploma.Common.ValidationServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Diploma.WebAssembly;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Diploma.WebAssembly.BusinessLogic.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var webAssemblyHostBuilder = WebAssemblyHostBuilder.CreateDefault(args);

webAssemblyHostBuilder.RootComponents.Add<App>("#app");

webAssemblyHostBuilder.RootComponents.Add<HeadOutlet>("head::after");

webAssemblyHostBuilder.Services.AddMudServices();

webAssemblyHostBuilder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7095/api/")
});

webAssemblyHostBuilder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();

webAssemblyHostBuilder.Services.AddAuthorizationCore();

webAssemblyHostBuilder.Services.AddBlazoredLocalStorage();

webAssemblyHostBuilder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserValidationService, UserValidationService>();

webAssemblyHostBuilder.Services.AddScoped<IMatchService, MatchService>();

webAssemblyHostBuilder.Services
    .AddScoped<ITeamService, TeamService>()
    .AddScoped<ITeamValidationService, TeamValidationService>();

webAssemblyHostBuilder.Services.AddScoped<ITournamentService, TournamentService>();

await webAssemblyHostBuilder
    .Build()
    .RunAsync();