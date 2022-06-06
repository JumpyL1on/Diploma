using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder
            .HasOne(x => x.TeamMember)
            .WithOne(x => x.User)
            .HasForeignKey<TeamMember>(x => x.UserId)
            .IsRequired();

        builder
            .HasOne(x => x.OrganizationMember)
            .WithOne(x => x.User)
            .HasForeignKey<OrganizationMember>(x => x.UserId)
            .IsRequired();
    }
}