using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PetStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
        public ICollection<Food> Food { get; set; } = new HashSet<Food>();
        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();
    }
}
