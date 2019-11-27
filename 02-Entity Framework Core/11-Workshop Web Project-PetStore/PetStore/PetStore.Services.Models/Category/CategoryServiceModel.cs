using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Models.Category
{
    public class CategoryServiceModel
    {
        public string Name { get; set; }

        public int CategoryToysCount { get; set; }

        public int CategoryFoodCount { get; set; }

        public int CategoryPetsCount { get; set; }
    }
}
