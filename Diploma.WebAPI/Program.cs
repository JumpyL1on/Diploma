using System.Text;
using Diploma.WebAPI.BusinessLogic;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Profiles;
using Diploma.WebAPI.BusinessLogic.Services;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Diploma.WebAPI.DataAccess.ValueObjects;
using Diploma.WebAPI.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddDbContext<AppDbContext>();

webApplicationBuilder.Services.AddAutoMapper(typeof(MatchProfile).Assembly);

webApplicationBuilder.Services
    .AddIdentityCore<User>(identityOptions =>
    {
        identityOptions.User.RequireUniqueEmail = true;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

webApplicationBuilder.Services.AddServices();

webApplicationBuilder.Services.AddValidationServices();

webApplicationBuilder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = webApplicationBuilder.Configuration.GetSection("JWTSettings").Get<JWTSettings>();
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
        };
    });

webApplicationBuilder.Services.AddControllers();

webApplicationBuilder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

webApplicationBuilder.Services.AddHangfire(globalConfiguration =>
{
    var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");
    globalConfiguration.UsePostgreSqlStorage(connectionString);
});

webApplicationBuilder.Services.AddHangfireServer();

var webApplication = webApplicationBuilder.Build();

if (webApplication.Environment.IsDevelopment())
{
    webApplication
        .UseSwagger()
        .UseSwaggerUI();

    webApplication.UseCors(corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
}

webApplication.UseHttpsRedirection();

webApplication.UseRouting();

webApplication.UseAuthentication();

webApplication.UseAuthorization();

webApplication.MapControllers();

webApplication.UseHangfireDashboard();

await webApplication.RunAsync();