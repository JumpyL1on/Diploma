using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.ToTable("TeamMember");
        
        builder
            .Property(teamMember => teamMember.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(teamMember => teamMember.Role)
            .IsRequired();
    }
}