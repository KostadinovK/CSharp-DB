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

        IEnumerable<BrandModel> GetAllBrands();
    }
}
