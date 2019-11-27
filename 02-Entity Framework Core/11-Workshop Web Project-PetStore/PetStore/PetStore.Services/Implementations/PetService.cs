using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Pet;

namespace PetStore.Services.Implementations
{
    public class PetService : Service, IPetService
    {
        public PetService(PetStoreDbContext context) : base(context)
        {
        }

        public void BuyFromDistributor(string name, int categoryId, string breed, string genre, decimal price, decimal profit,
            DateTime birthDate)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Pet Name!");
            }

            if (categoryId < 1 || categoryId > context.Categories.Count())
            {
                throw new ArgumentException("Invalid category Id!");
            }

            if (profit < 0 || profit > 5)
            {
                throw new ArgumentException("Profit must be between 0 and 500%!");
            }

            if (genre != "Male" && genre != "Female")
            {
                throw new ArgumentException("Invalid pet genre!");
            }

            var pet = new Pet()
            {
                Name = name,
                CategoryId = categoryId,
                DistributorPrice = price,
                Price = price * profit,
                BirthDate = birthDate,
                Breed = breed,
                Genre = Enum.Parse<Genre>(genre)
            };

            if (!IsValid(pet))
            {
                throw new InvalidOperationException("Invalid pet!");
            }

            context.Pets.Add(pet);
            context.SaveChanges();
        }

        public void Sell(int petId, int userId, string description)
        {
            if (petId < 1 || petId > context.Food.Count())
            {
                throw new ArgumentException("Invalid food Id!");
            }

            if (userId < 1 || userId > context.Users.Count())
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var pet = context.Pets.Find(petId);
            var user = context.Users.Find(userId);

            if (pet.OrderId != null)
            {
                throw new InvalidOperationException("Pet has already a owner!");
            }

            var order = new Order()
            {
                UserId = userId,
                User = user,
                Description = description,
                OrderStatus = OrderStatus.Completed
            };

            context.Orders.Add(order);

            pet.Order = order;

            context.SaveChanges();
        }

        public IEnumerable<PetListingModel> GetAllToysByCategory(string categoryName)
        {
            if (String.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Invalid category Name!");
            }

            return context.Pets
                .Where(p => p.Category.Name.ToLower().Contains(categoryName.ToLower()))
                .Select(p => new PetListingModel()
                {
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    BirthDate = p.BirthDate,
                    Breed = p.Breed,
                    Genre = p.Genre.ToString()
                })
                .ToList();
        }

        public void Remove(int id)
        {
            if (id < 1 || id > context.Pets.Count())
            {
                throw new ArgumentException("Invalid pet Id!");
            }

            var pet = context.Pets.Find(id);

            context.Pets.Remove(pet);
            context.SaveChanges();
        }
    }
}
