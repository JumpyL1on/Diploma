using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> entityTypeBuilder)
    {
        entityTypeBuilder
            .Property(teamMember => teamMember.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entityTypeBuilder
            .Property(teamMember => teamMember.Role)
            .IsRequired();

        entityTypeBuilder
            .HasOne(teamMember => teamMember.User)
            .WithOne(user => user.TeamMember)
            .HasForeignKey<TeamMember>(teamMember => teamMember.UserId)
            .IsRequired();

        entityTypeBuilder
            .HasOne(teamMember => teamMember.Team)
            .WithMany(team => team.TeamMembers)
            .HasForeignKey(teamMember => teamMember.TeamId)
            .IsRequired();
    }
}