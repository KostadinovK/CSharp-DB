using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Food;

namespace PetStore.Services.Implementations
{
    public class FoodService : Service, IFoodService
    {
        public FoodService(PetStoreDbContext context) : base(context)
        {
        }

        public void BuyFromDistributor(int brandId, string name, int categoryId, decimal price, decimal profit,
            DateTime expirationDate, double weight)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Food Name!");
            }

            if (brandId < 1 || brandId > context.Brands.Count())
            {
                throw new ArgumentException("Invalid brand Id!");
            }

            if (categoryId < 1 || categoryId > context.Categories.Count())
            {
                throw new ArgumentException("Invalid category Id!");
            }

            if (profit < 0 || profit > 5)
            {
                throw new ArgumentException("Profit must be between 0 and 500%!");
            }

            var food = new Food()
            {
                BrandId = brandId,
                Name = name,
                CategoryId = categoryId,
                DistributorPrice = price,
                Price = price * profit,
                ExpirationDate = expirationDate,
                Weight = weight
            };

            if (!IsValid(food))
            {
                throw new InvalidOperationException("Invalid food!");
            }

            context.Food.Add(food);
            context.SaveChanges();
        }

        public void Sell(int foodId, int userId, string description)
        {
            if (foodId < 1 || foodId > context.Food.Count())
            {
                throw new ArgumentException("Invalid food Id!");
            }

            if (userId < 1 || userId > context.Users.Count())
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var user = context.Users.Find(userId);

            var order = new Order()
            {
                UserId = userId,
                User = user,
                Description = description,
                OrderStatus = OrderStatus.Completed
            };

            context.Orders.Add(order);

            var foodOrder = new FoodOrders()
            {
                Order = order,
                OrderId = order.Id,
                Food = context.Food.Find(foodId),
                FoodId = foodId
            };

            context.FoodOrders.Add(foodOrder);
            context.SaveChanges();
        }

        public IEnumerable<FoodListingModel> GetAllToysByBrand(string brandName)
        {
            if (String.IsNullOrWhiteSpace(brandName))
            {
                throw new ArgumentException("Invalid brand Name!");
            }

            return context.Food
                .Where(f => f.Brand.Name.ToLower().Contains(brandName.ToLower()))
                .Select(f => new FoodListingModel()
                {
                    BrandName = f.Brand.Name,
                    Name = f.Name,
                    Price = f.Price,
                    CategoryName = f.Category.Name,
                    ExpirationDate = f.ExpirationDate,
                    Weight = f.Weight
                })
                .ToList();
        }

        public IEnumerable<FoodListingModel> GetAllToysByCategory(string categoryName)
        {
            if (String.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Invalid brand Name!");
            }

            return context.Food
                .Where(f => f.Category.Name.ToLower().Contains(categoryName.ToLower()))
                .Select(f => new FoodListingModel()
                {
                    BrandName = f.Brand.Name,
                    Name = f.Name,
                    Price = f.Price,
                    CategoryName = f.Category.Name,
                    ExpirationDate = f.ExpirationDate,
                    Weight = f.Weight
                })
                .ToList();
        }

        public void Remove(int id)
        {
            if (id < 1 || id > context.Food.Count())
            {
                throw new ArgumentException("Invalid food Id!");
            }

            var food = context.Food.Find(id);

            context.Food.Remove(food);
            context.SaveChanges();
        }
    }
}
