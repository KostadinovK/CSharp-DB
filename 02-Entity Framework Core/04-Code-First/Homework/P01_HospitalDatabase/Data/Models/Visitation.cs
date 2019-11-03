using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataValidation.Visitation;

    public class Visitation
    {
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(MaxCommentsLength)]
        public string Comments { get; set; }

        [Column("Patient")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
