using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder
            .HasOne(match => match.ParticipantA)
            .WithMany(participant => participant.ParticipantAMatches)
            .HasForeignKey(match => match.ParticipantAId)
            .IsRequired(false);
        
        builder
            .HasOne(match => match.ParticipantB)
            .WithMany(participant => participant.ParticipantBMatches)
            .HasForeignKey(match => match.ParticipantBId)
            .IsRequired(false);
    }
}