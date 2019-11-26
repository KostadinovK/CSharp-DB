using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Models.Enums;

namespace PetStore.Models
{
    public class Pet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Breed { get; set; }

        public DateTime BirthDate { get; set; }

        public Genre Genre { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
