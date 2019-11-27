using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Models.Food
{
    public class FoodListingModel
    {
        public string BrandName { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public double Weight { get; set; }
    }
}
