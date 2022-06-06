using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");
        
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .HasMany(x => x.Teams)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId)
            .IsRequired();

        builder
            .HasMany(x => x.Tournaments)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId)
            .IsRequired();
    }
}