using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Services.Models.Food;

namespace PetStore.Services.Interfaces
{
    public interface IFoodService
    {
        void BuyFromDistributor(int brandId, string name, int categoryId, decimal price, decimal profit, DateTime expirationDate, double weight);

        void Sell(int foodId, int userId, string description);

        IEnumerable<FoodListingModel> GetAllToysByBrand(string brandName);
        IEnumerable<FoodListingModel> GetAllToysByCategory(string categoryName);

        void Remove(int id);
    }
}
