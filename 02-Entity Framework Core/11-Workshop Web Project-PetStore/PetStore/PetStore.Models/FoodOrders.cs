using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class FoodOrders
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
