using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.WebAPI.DataAccess.EntityConfigurations;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder.ToTable("UserGame");
        
        builder
            .Property(x => x.Nickname)
            .IsRequired();

        builder
            .Property(x => x.SteamId)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserGames)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.HasKey(x => new { x.UserId, x.GameId });

        builder
            .HasOne(x => x.Game)
            .WithMany(x => x.UserGames)
            .HasForeignKey(x => x.GameId)
            .IsRequired();
    }
}