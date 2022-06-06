using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Match");
        
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Start)
            .IsRequired(false);

        builder
            .Property(x => x.FinishedAt)
            .IsRequired(false);

        builder
            .Property(x => x.Round)
            .IsRequired();

        builder
            .Property(x => x.Order)
            .IsRequired();

        builder
            .Property(x => x.LeftTeamScore)
            .IsRequired();

        builder
            .Property(x => x.RightTeamScore)
            .IsRequired();
    }
}