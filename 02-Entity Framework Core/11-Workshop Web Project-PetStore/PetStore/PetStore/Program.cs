using System;
using PetStore.Data;
using PetStore.Services.Implementations;

namespace PetStore
{
    class Program
    {
        static void Main()
        {
            var context = new PetStoreDbContext();

            var brandService = new BrandService(context);
            Console.WriteLine(brandService.Create("Intel"));

            var brands = brandService.GetAllBrands();
            foreach (var brand in brands)
            {
                Console.WriteLine(brand.Name);
            }
        }
    }
}
