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
                throw new ArgumentException("Brand name cannot be null!");
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

        public BrandModel GetById(int id)
        {
            if (id < 1 || id > context.Brands.Count())
            {
                throw new ArgumentException("Invalid brand Id");
            }

            var brand = context.Brands.Find(id);

            return new BrandModel()
            {
                Name = brand.Name,
                ToysCount = brand.Toys.Count,
                FoodsCount = brand.Food.Count
            };
        }

        public void RemoveById(int id)
        {
            if (id < 1 || id > context.Brands.Count())
            {
                throw new ArgumentException("Invalid brand Id");
            }

            var brand = context.Brands.Find(id);

            context.Brands.Remove(brand);
            context.SaveChanges();
        }

        public void Remove(BrandModel brand)
        {
            if (String.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentException("Brand name cannot be null!");
            }

            var brandToRemove = context.Brands
                .FirstOrDefault(b => b.Name == brand.Name);

            if (brandToRemove == null)
            {
                throw new InvalidOperationException("No such brand exists!");
            }

            context.Brands.Remove(brandToRemove);
            context.SaveChanges();
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

        public IEnumerable<BrandModel> Search(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Invalid brand name!");
            }

            return context.Brands
                .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                .Select(b => new BrandModel()
                {
                    Name = b.Name,
                    ToysCount = b.Toys.Count,
                    FoodsCount = b.Food.Count
                })
                .ToList();
        }
    }
}
