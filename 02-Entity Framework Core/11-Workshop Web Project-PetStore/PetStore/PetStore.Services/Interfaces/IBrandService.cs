using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using PetStore.Services.Models;

namespace PetStore.Services.Interfaces
{
    public interface IBrandService
    {
        int Create(string name);

        BrandModel GetById(int id);

        void RemoveById(int id);

        void Remove(BrandModel brand);

        IEnumerable<BrandModel> GetAllBrands();

        IEnumerable<BrandModel> Search(string name);
    }
}
