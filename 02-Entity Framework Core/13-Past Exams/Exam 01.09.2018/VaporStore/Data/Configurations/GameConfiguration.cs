using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game.HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId);

            game.HasOne(g => g.Genre)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.GenreId);

            game.HasMany(g => g.Purchases)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId);
        }
    }
}
