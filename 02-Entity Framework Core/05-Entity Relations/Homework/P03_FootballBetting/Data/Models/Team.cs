using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    using static DataValidation.Team;

    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        [MaxLength(MaxInitialsLength)]
        public string Initials { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [Required]
        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }

        [Required]
        public int SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; }

        [Required]
        public int TownId { get; set; }

        public Town Town { get; set; }

        [InverseProperty("HomeTeam")]
        public ICollection<Game> HomeGames { get; set; } = new HashSet<Game>();

        [InverseProperty("AwayTeam")]
        public ICollection<Game> AwayGames { get; set; } = new HashSet<Game>();

        public ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}
