using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetStore.Models.Enums;

namespace PetStore.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Breed { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
