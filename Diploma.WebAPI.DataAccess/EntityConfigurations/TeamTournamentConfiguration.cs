using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TeamTournamentConfiguration : IEntityTypeConfiguration<TeamTournament>
{
    public void Configure(EntityTypeBuilder<TeamTournament> builder)
    {
        builder.ToTable("TeamTournament");

        builder
            .HasOne(x => x.Team)
            .WithMany(x => x.TeamTournaments)
            .HasForeignKey(x => x.TeamId)
            .IsRequired();

        builder.HasKey(x => new { x.TeamId, x.TournamentId });

        builder
            .HasOne(x => x.Tournament)
            .WithMany(x => x.TeamTournaments)
            .HasForeignKey(x => x.TournamentId)
            .IsRequired();

        builder
            .Property(x => x.AchievedPlace)
            .IsRequired(false);
    }
}