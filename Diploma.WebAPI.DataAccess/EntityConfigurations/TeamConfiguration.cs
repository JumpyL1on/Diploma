using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> entityTypeBuilder)
    {
        entityTypeBuilder
            .Property(team => team.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entityTypeBuilder
            .Property(team => team.Title)
            .IsRequired();
    }
}