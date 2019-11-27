using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Models.Pet
{
    public class PetListingModel
    {
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Breed { get; set; }

        public DateTime BirthDate { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}
