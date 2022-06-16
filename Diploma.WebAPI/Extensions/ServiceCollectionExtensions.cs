using Diploma.Common.Interfaces;
using Diploma.Common.Services;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Services;
using Diploma.WebAPI.BusinessLogic.SteamGameClient;
using SteamKit2;

namespace Diploma.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<IMatchService, MatchService>()
            .AddScoped<IOrganizationMemberService, OrganizationMemberService>()
            .AddScoped<IOrganizationService, OrganizationService>()
            .AddScoped<ITeamTournamentService, TeamTournamentService>()
            .AddScoped<ITeamMemberService, TeamMemberService>()
            .AddScoped<ITeamService, TeamService>()
            .AddScoped<ITournamentService, TournamentService>()
            .AddScoped<IUserGameService, UserGameService>()
            .AddScoped<IUserService, UserService>()
            .AddTransient<IJwtService, JwtService>();
    }

    public static void AddValidationServices(this IServiceCollection services)
    {
        services
            .AddTransient<IOrganizationValidationService, OrganizationValidationService>()
            .AddTransient<ITeamValidationService, TeamValidationService>()
            .AddTransient<ITournamentValidationService, TournamentValidationService>()
            .AddTransient<IUserValidationService, UserValidationService>();
    }

    public static SteamGameClient AddSteamGameClient(this IServiceCollection services)
    {
        var steamClient = new SteamClient();

        steamClient.AddHandler(new SteamGameClient(steamClient));

        var steamGameClient = steamClient.GetHandler<SteamGameClient>();

        services.AddSingleton(steamGameClient!);

        return steamGameClient!;
    }
}