using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P03_FootballBetting.Data.Enums;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        [Required]
        public int HomeTeamGoals { get; set; }

        [Required]
        public int AwayTeamGoals { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public double HomeTeamBetRate { get; set; }

        [Required]
        public double AwayTeamBetRate { get; set; }

        [Required]
        public double DrawBetRate { get; set; }

        [Required]
        public ResultType Result { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}
