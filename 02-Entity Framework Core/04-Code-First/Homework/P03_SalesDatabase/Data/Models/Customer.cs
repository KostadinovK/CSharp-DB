using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    using static DataValidation.Customer;

    public class Customer
    {
        public int CustomerId { get; set; }

        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [MaxLength(MaxEmailLength)]
        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
