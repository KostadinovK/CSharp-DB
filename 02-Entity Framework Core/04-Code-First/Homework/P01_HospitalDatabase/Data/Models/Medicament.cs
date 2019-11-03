using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataValidation.Medicament;

    public class Medicament
    {
        public int MedicamentId { get; set; }
    
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; } = new HashSet<PatientMedicament>();
    }
}
