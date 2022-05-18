using Diploma.Common.Interfaces;
using Diploma.Common.ValidationServices;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Services;

namespace Diploma.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IJWTService, JWTService>();
        
        services.AddScoped<IMatchService, MatchService>();

        services.AddScoped<IParticipantService, ParticipantService>();
        
        services.AddScoped<ITeamService, TeamService>();

        services.AddScoped<ITeamMemberService, TeamMemberService>();

        services.AddScoped<ITournamentService, TournamentService>();

        services.AddScoped<IUserService, UserService>();
    }

    public static void AddValidationServices(this IServiceCollection services)
    {
        services.AddTransient<ITeamValidationService, TeamValidationService>();

        services.AddTransient<IUserValidationService, UserValidationService>();
    }
}