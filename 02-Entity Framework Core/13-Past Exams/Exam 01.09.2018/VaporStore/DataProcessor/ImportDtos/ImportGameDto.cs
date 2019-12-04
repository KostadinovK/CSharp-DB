using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDtos
{
    public class ImportGameDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required] 
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
