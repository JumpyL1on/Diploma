using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");
        
        builder
            .Property(team => team.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(team => team.Title)
            .IsRequired();

        builder
            .HasMany(x => x.TeamMembers)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId)
            .IsRequired();

        builder
            .HasMany(x => x.LeftTeamMatches)
            .WithOne(x => x.LeftTeam)
            .HasForeignKey(x => x.LeftTeamId)
            .IsRequired(false);

        builder
            .HasMany(x => x.RightTeamMatches)
            .WithOne(x => x.RightTeam)
            .HasForeignKey(x => x.RightTeamId)
            .IsRequired(false);
    }
}