using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> playerStat)
        {
            playerStat
                .HasKey(ps => new {ps.GameId, ps.PlayerId});

            playerStat
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId);

            playerStat
                .HasOne(ps => ps.Game)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);
        }
    }
}
