using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetStore.Models.Enums;

namespace PetStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<FoodOrders> OrdersFood { get; set; } = new HashSet<FoodOrders>();
        public ICollection<ToysOrders> OrdersToys { get; set; } = new HashSet<ToysOrders>();

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}
