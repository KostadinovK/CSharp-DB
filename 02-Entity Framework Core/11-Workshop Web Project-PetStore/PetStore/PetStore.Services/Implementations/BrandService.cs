using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models;
using PetStore.Services.Interfaces;
using PetStore.Services.Models;

namespace PetStore.Services.Implementations
{
    public class BrandService : Service, IBrandService
    {
        public BrandService(PetStoreDbContext context) : base(context)
        {
        }

        public int Create(string name)
        {
            if(name == null)
            {
                throw new ArgumentNullException("Brand name cannot be null!");
            }

            if (context.Brands.Any(b => b.Name == name))
            {
                throw new InvalidOperationException("This brand name already exists!");
            }

            var brand = new Brand(){
                Name = name
            };

            if (!IsValid(brand))
            {
                throw new ArgumentException("Brand is invalid!");
            }

            context.Brands.Add(brand);
            context.SaveChanges();

            return brand.Id;
        }

        public IEnumerable<BrandModel> GetAllBrands()
        {
            var brands = context.Brands
                .OrderBy(b => b.Name)
                .Select(b => new BrandModel()
                {
                    Name = b.Name,
                    ToysCount = b.Toys.Count,
                    FoodsCount = b.Food.Count
                })
                .ToList();

            return brands;
        }
    }
}
