using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Models;
using PetStore.Services.Models.Toy;

namespace PetStore.Services.Interfaces
{
    public interface IToyService
    {
        void BuyFromDistributor(int brandId, string name, int categoryId, decimal price, decimal profit);

        void Sell(int toyId, int userId, string description);

        IEnumerable<ToyListingModel> GetAllToysByBrand(string brandName);
        IEnumerable<ToyListingModel> GetAllToysByCategory(string categoryName);

        void Remove(int id);
    }
}
