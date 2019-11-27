using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Services.Models.Category;

namespace PetStore.Services.Interfaces
{
    public interface ICategoryService
    {
        int Add(string name);

        CategoryServiceModel GetById(int id);

        void RemoveById(int id);

        void Remove(CategoryServiceModel category);

        IEnumerable<CategoryServiceModel> GetAllCategories();

        IEnumerable<CategoryServiceModel> Search(string name);
    }
}
