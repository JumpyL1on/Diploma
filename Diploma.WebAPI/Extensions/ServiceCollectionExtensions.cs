using Diploma.Common.Interfaces;
using Diploma.Common.Services;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Services;

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
            .AddScoped<IParticipantService, ParticipantService>()
            .AddScoped<ITeamMemberService, TeamMemberService>()
            .AddScoped<ITeamService, TeamService>()
            .AddScoped<ITournamentService, TournamentService>()
            .AddScoped<IUserGameService, UserGameService>()
            .AddScoped<IUserService, UserService>();

        services.AddTransient<IJwtService, JwtService>();
    }

    public static void AddValidationServices(this IServiceCollection services)
    {
        services
            .AddTransient<IOrganizationValidationService, OrganizationValidationService>()
            .AddTransient<ITeamValidationService, TeamValidationService>()
            .AddTransient<IUserValidationService, UserValidationService>();
    }
}