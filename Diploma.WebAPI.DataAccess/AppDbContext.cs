using System.Reflection;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diploma.WebAPI.DataAccess;

public class AppDbContext : IdentityUserContext<User, Guid>
{
    private readonly ILoggerFactory _loggerFactory =
        LoggerFactory.Create(loggingBuilder =>
        {
            loggingBuilder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
        });

    public AppDbContext()
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Match> Matches { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql("Host=localhost;Port=5432;Database=diploma;Username=postgres;Password=password")
            .UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}