using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetStore.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public double Weight { get; set; }

        [Required]
        [Range(0, 10000d)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10000d)]
        public decimal DistributorPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public ICollection<FoodOrders> OrdersFood { get; set; } = new HashSet<FoodOrders>();
    }
}
