using System;
using System.Collections.Generic;
using PetStore.Services.Models.Pet;

namespace PetStore.Services.Interfaces
{
    public interface IPetService
    {
        void BuyFromDistributor(string name, int categoryId, string breed, string genre, decimal price, decimal profit, DateTime birthDate);

        void Sell(int petId, int userId, string description);

        IEnumerable<PetListingModel> GetAllToysByCategory(string categoryName);

        void Remove(int id);
    }
}
