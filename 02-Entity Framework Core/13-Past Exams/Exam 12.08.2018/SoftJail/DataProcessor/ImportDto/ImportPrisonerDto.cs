﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerDto
    {
        [MinLength(3), MaxLength(20), Required]
        public string FullName { get; set; }

        [RegularExpression(@"^The [A-Z]{1}[a-z]+$"), Required]
        public string Nickname { get; set; }

        [Range(18, 65), Required]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public List<ImportMailDto> Mails { get; set; } = new List<ImportMailDto>();
    }
}
