using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        [MinLength(3), MaxLength(30), Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue), Required]
        public decimal Money { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Weapon { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [XmlArray]
        public List<ImportOfficerPrisonerDto> Prisoners { get; set; } = new List<ImportOfficerPrisonerDto>();
    }
}
