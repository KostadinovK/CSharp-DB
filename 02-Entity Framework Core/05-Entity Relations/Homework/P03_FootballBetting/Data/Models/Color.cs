using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    using static DataValidation.Color;

    public class Color
    {
        public int ColorId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [InverseProperty("PrimaryKitColor")]
        public ICollection<Team> PrimaryKitTeams { get; set; } = new HashSet<Team>();
        [InverseProperty("SecondaryKitColor")]
        public ICollection<Team> SecondaryKitTeams { get; set; } = new HashSet<Team>();
    }
}
