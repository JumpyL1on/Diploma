using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("Tournament");

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .Property(x => x.ParticipantsNumber)
            .IsRequired();

        builder
            .Property(x => x.MaxParticipantsNumber)
            .IsRequired();

        builder
            .Property(x => x.Start)
            .IsRequired();

        builder
            .Property(x => x.FinishedAt)
            .IsRequired(false);

        builder
            .HasMany(x => x.Matches)
            .WithOne(x => x.Tournament)
            .HasForeignKey(x => x.TournamentId)
            .IsRequired();
    }
}