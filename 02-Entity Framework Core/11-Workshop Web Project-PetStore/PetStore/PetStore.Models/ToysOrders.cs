using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class ToysOrders
    {
        public int ToyId { get; set; }
        public Toy Toy { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
