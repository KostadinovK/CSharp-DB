﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    using static DataValidation.Country;

    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; } = new HashSet<Town>();
    }
}
