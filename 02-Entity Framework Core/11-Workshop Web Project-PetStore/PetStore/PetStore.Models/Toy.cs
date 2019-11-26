using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class Toy
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal Price { get; set; }

        public ICollection<ToysOrders> ToyOrders { get; set; } = new HashSet<ToysOrders>();
    }
}
