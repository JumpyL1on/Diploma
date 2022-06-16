using Diploma.WebAPI.DataAccess.Entities;
using Diploma.WebAPI.DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diploma.WebAPI.DataAccess;

public class AppDbContext : IdentityUserContext<User, Guid>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationMember> OrganizationMembers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamTournament> TeamTournaments { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<UserGame> UserGames { get; set; }

    private readonly ILoggerFactory _loggerFactory =
        LoggerFactory.Create(loggingBuilder =>
        {
            loggingBuilder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
        });

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql("Host=localhost;Port=5432;Database=diploma;Username=postgres;Password=password");
        //.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
    }
}