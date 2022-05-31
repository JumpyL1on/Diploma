using System.Text;
using Diploma.WebAPI;
using Diploma.WebAPI.BusinessLogic.Profiles;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Diploma.WebAPI.DataAccess.ValueObjects;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddAutoMapper(typeof(MatchProfile).Assembly);

builder.Services
    .AddIdentityCore<User>(identityOptions =>
    {
        identityOptions.User.RequireUniqueEmail = true;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddServices();

builder.Services.AddValidationServices();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration
            .GetSection("JWTSettings")
            .Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
        };
    })
    .AddCookie()
    .AddSteam(options => options.ApplicationKey = "2299E12A058C57E16AD5661BD11E5F84");

builder.Services.AddControllers(options => { options.Filters.Add(typeof(ExceptionFilter)); });

builder.Services.AddMvcCore();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

/*webApplicationBuilder.Services.AddHangfire(globalConfiguration =>
{
    var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");
    globalConfiguration.UsePostgreSqlStorage(connectionString);
});

webApplicationBuilder.Services.AddHangfireServer();*/

var webApplication = builder.Build();

if (webApplication.Environment.IsDevelopment())
{
    webApplication
        .UseSwagger()
        .UseSwaggerUI();

    webApplication.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
}

webApplication.UseHttpsRedirection();

webApplication.UseRouting();

webApplication.UseAuthentication();

webApplication.UseAuthorization();

webApplication.MapControllers();

//webApplication.UseHangfireDashboard();

await webApplication.RunAsync();