using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        [Required]
        [ForeignKey(nameof(Officer))]
        public int OfficerId { get; set; }

        [Required]
        [ForeignKey(nameof(Prisoner))]
        public int PrisonerId { get; set; }

        public Officer Officer { get; set; }

        public Prisoner Prisoner { get; set; }
    }
}
