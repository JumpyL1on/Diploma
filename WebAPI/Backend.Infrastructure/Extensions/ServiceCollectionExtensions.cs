using Backend.Application.Interfaces;
using Backend.Core.Entities;
using Backend.Infrastructure.Behaviours;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Middlewares;
using Backend.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services
            .AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.ClaimsIdentity.UserIdClaimType = "Id";
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<DbContext, AppDbContext>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddScoped<IJWTService, JWTService>();
    }
}