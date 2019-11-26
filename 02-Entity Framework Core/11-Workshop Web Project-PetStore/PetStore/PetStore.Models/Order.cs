using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<FoodOrders> OrdersFood { get; set; } = new HashSet<FoodOrders>();
        public ICollection<ToysOrders> OrdersToys { get; set; } = new HashSet<ToysOrders>();

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}
