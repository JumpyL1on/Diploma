using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organization");

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .IsRequired();
    }
}