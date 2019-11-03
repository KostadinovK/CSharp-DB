using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    using static DataValidation.Diagnose;

    public class Diagnose
    { 
        public int DiagnoseId { get; set; }

        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [MaxLength(MaxCommentsLength)]
        public string Comments { get; set; }

        [Column("Patient")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
