using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Services.Models.User;

namespace PetStore.Services.Interfaces
{
    public interface IUserService
    {
        int Register(UserViewModel user);

        UserViewModel GetUserById(int id);

        void DeleteUser(int userId);
    }
}
