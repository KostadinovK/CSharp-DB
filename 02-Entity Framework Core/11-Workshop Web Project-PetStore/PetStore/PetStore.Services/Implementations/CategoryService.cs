using System;
using System.Collections.Generic;
using System.Linq;
using PetStore.Data;
using PetStore.Models;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Category;

namespace PetStore.Services.Implementations
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(PetStoreDbContext context) : base(context)
        {
        }

        public int Add(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Category Name!");
            }

            if (context.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                throw new InvalidOperationException("This Category already exists!");
            }

            var category = new Category()
            {
                Name = name,
            };

            if (!IsValid(category))
            {
                throw new InvalidOperationException("Category Name is invalid!");
            }

            context.Categories.Add(category);
            context.SaveChanges();

            return category.Id;
        }

        public CategoryServiceModel GetById(int id)
        {
            if (id < 1 || id > context.Brands.Count())
            {
                throw new ArgumentException("Invalid brand Id");
            }

            var category = context.Categories.Find(id);

            return new CategoryServiceModel()
            {
                Name = category.Name,
                CategoryToysCount = category.Toys.Count,
                CategoryFoodCount = category.Food.Count,
                CategoryPetsCount = category.Pets.Count
            };
        }

        public void RemoveById(int id)
        {
            if (id < 1 || id > context.Categories.Count())
            {
                throw new ArgumentException("Invalid category Id");
            }

            var category = context.Categories.Find(id);

            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void Remove(CategoryServiceModel category)
        {
            if (String.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category name cannot be null!");
            }

            var categoryToRemove = context.Categories
                .FirstOrDefault(c => c.Name == category.Name);

            if (categoryToRemove == null)
            {
                throw new InvalidOperationException("No such category exists!");
            }

            context.Categories.Remove(categoryToRemove);
            context.SaveChanges();
        }

        public IEnumerable<CategoryServiceModel> GetAllCategories()
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryServiceModel()
                {
                    Name = c.Name,
                    CategoryToysCount = c.Toys.Count,
                    CategoryFoodCount = c.Food.Count,
                    CategoryPetsCount = c.Pets.Count
                })
                .ToList();

            return categories;
        }

        public IEnumerable<CategoryServiceModel> Search(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Invalid Category name!");
            }

            return context.Categories
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new CategoryServiceModel()
                {
                    Name = c.Name,
                    CategoryToysCount = c.Toys.Count,
                    CategoryFoodCount = c.Food.Count,
                    CategoryPetsCount = c.Pets.Count
                })
                .ToList();
        }
    }
}
