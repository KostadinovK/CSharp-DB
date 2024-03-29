﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Castle.Components.DictionaryAdapter;

namespace CarDealer.Dtos.Import
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public List<ImportPartIdDto> Parts { get; set; } = new List<ImportPartIdDto>();
    }
}
