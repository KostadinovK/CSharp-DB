using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetStore.Models
{
    public class Toy
    {
        public int Id { get; set; }

        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Range(0, 10000d)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10000d)]
        public decimal DistributorPrice { get; set; }

        public ICollection<ToysOrders> ToyOrders { get; set; } = new HashSet<ToysOrders>();
    }
}
