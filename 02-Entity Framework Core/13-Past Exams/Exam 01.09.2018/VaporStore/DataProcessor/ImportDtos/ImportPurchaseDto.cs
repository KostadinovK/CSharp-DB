using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enum;

namespace VaporStore.DataProcessor.ImportDtos
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Z]{4}-[0-9A-Z]{4}-[0-9A-Z]{4}$")]
        public string Key { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Card { get; set; }

        [XmlAttribute("title")]
        [Required]
        public string Game { get; set; }
    }
}
