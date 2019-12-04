using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configurations
{
    public class GameTagConfiguration : IEntityTypeConfiguration<GameTag>
    {
        public void Configure(EntityTypeBuilder<GameTag> gameTag)
        {
            gameTag.HasKey(gt => new {gt.GameId, gt.TagId});

            gameTag.HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagId);

            gameTag.HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameId);
        }
    }
}
