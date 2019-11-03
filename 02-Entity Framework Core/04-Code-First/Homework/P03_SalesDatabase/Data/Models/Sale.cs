using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    
    public class Sale
    {
        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        [Column("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; } 

        [Column("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Column("Store")]
        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
