using System.Text;
using Diploma.WebAPI;
using Diploma.WebAPI.BusinessLogic.Profiles;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Diploma.WebAPI.DataAccess.ValueObjects;
using Diploma.WebAPI.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddAutoMapper(typeof(MatchProfile).Assembly);

builder.Services
    .AddIdentityCore<User>(x => x.User.RequireUniqueEmail = true)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddServices();

builder.Services.AddValidationServices();

var client = builder.Services.AddSteamGameClient();

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        x.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
        };

        x.MapInboundClaims = false;
    })
    .AddCookie()
    .AddSteam(x => x.ApplicationKey = "2299E12A058C57E16AD5661BD11E5F84");

builder.Services.AddControllers(x => x.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddMvcCore();

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

builder.Services.AddHangfire(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    x.UsePostgreSqlStorage(connectionString);
});

builder.Services.AddHangfireServer();

var application = builder.Build();

application.Lifetime.ApplicationStopped.Register(() => client.Dispose());

if (application.Environment.IsDevelopment())
{
    application.UseSwagger().UseSwaggerUI();

    application.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
}

application.UseHttpsRedirection();

application.UseRouting();

application.UseAuthentication();

application.UseAuthorization();

application.MapControllers();

application.UseHangfireDashboard();

await application.RunAsync();