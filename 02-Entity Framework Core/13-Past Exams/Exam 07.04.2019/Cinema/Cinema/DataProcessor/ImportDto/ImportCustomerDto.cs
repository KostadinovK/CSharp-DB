using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class ImportCustomerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public decimal Balance { get; set; }

        [XmlArray]
        public List<ImportTicketDto> Tickets { get; set; } = new List<ImportTicketDto>();
    }
}
