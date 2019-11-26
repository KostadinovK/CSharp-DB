using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Food> Food { get; set; } = new HashSet<Food>();

        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();
    }
}
