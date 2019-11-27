using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Toy;

namespace PetStore.Services.Implementations
{
    public class ToyService : Service, IToyService
    {
        public ToyService(PetStoreDbContext context) : base(context)
        {
        }

        public void BuyFromDistributor(int brandId, string name, int categoryId, decimal price, decimal profit)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid toy Name!");
            }

            if(brandId < 1 || brandId > context.Brands.Count())
            {
                throw new ArgumentException("Invalid brand Id!");
            }

            if (categoryId < 1 || categoryId > context.Categories.Count())
            {
                throw new ArgumentException("Invalid category Id!");
            }

            if(profit < 0 || profit > 5)
            {
                throw new ArgumentException("Profit must be between 0 and 500%!");
            }

            var toy = new Toy()
            { 
                BrandId = brandId,
                Name = name,
                CategoryId = categoryId,
                DistributorPrice = price,
                Price = price * profit
            };

            if (!IsValid(toy))
            {
                throw new InvalidOperationException("Invalid toy!");
            }

            context.Toys.Add(toy);
            context.SaveChanges();
        }

        public void Sell(int toyId, int userId, string description)
        {
            if (toyId < 1 || toyId > context.Toys.Count())
            {
                throw new ArgumentException("Invalid toy Id!");
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

            var toyOrder = new ToysOrders()
            {
                Order = order,
                OrderId = order.Id,
                Toy = context.Toys.Find(toyId),
                ToyId = toyId
            };

            context.ToysOrders.Add(toyOrder);
            context.SaveChanges();
        }

        public IEnumerable<ToyListingModel> GetAllToysByBrand(string brandName)
        {
            if (String.IsNullOrWhiteSpace(brandName))
            {
                throw new ArgumentException("Invalid brand Name!");
            }

            return context.Toys
                .Where(t => t.Brand.Name.ToLower().Contains(brandName.ToLower()))
                .Select(t => new ToyListingModel()
                {
                    BrandName = t.Brand.Name,
                    Name = t.Name,
                    Price = t.Price,
                    CategoryName = t.Category.Name
                })
                .ToList();
        }

        public IEnumerable<ToyListingModel> GetAllToysByCategory(string categoryName)
        {
            if (String.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Invalid category Name!");
            }

            return context.Toys
                .Where(t => t.Category.Name.ToLower().Contains(categoryName.ToLower()))
                .Select(t => new ToyListingModel()
                {
                    BrandName = t.Brand.Name,
                    Name = t.Name,
                    Price = t.Price,
                    CategoryName = t.Category.Name
                })
                .ToList();
        }

        public void Remove(int id)
        {
            if (id < 1 || id > context.Toys.Count())
            {
                throw new ArgumentException("Invalid toy Id!");
            }

            var toy = context.Toys.Find(id);

            context.Toys.Remove(toy);
            context.SaveChanges();
        }
    }
}
