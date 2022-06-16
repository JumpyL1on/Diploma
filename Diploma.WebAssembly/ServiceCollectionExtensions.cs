using Diploma.Common.Interfaces;
using Diploma.Common.Services;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Diploma.WebAssembly.BusinessLogic.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Diploma.WebAssembly;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<IMatchService, MatchService>()
            .AddScoped<IOrganizationService, OrganizationService>()
            .AddScoped<ITeamTournamentService, TeamTournamentService>()
            .AddScoped<ITeamMemberService, TeamMemberService>()
            .AddScoped<ITeamService, TeamService>()
            .AddScoped<ITournamentService, TournamentService>()
            .AddScoped<IUserService, UserService>();

        services
            .AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>()
            .AddScoped<HttpInterceptor>();
    }

    public static void AddValidationServices(this IServiceCollection services)
    {
        services
            .AddTransient<IOrganizationValidationService, OrganizationValidationService>()
            .AddTransient<ITeamValidationService, TeamValidationService>()
            .AddTransient<ITournamentValidationService, TournamentValidationService>()
            .AddTransient<IUserValidationService, UserValidationService>();
    }
}