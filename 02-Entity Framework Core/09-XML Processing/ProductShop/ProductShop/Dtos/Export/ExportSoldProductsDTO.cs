using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    
    public class ExportSoldProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<ExportSoldProductDTO> SoldProducts { get; set; } = new List<ExportSoldProductDTO>();
    }
}
