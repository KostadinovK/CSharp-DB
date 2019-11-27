using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Models.Toy
{
    public class ToyListingModel
    {
        public string BrandName { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }
        
        public decimal Price { get; set; }
    }
}
