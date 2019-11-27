using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.User;

namespace PetStore.Services.Implementations
{
    public class UserService : Service, IUserService
    {
        public UserService(PetStoreDbContext context) : base(context)
        {
        }

        public int Register(UserViewModel user)
        {
            var userToAdd = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.RegisterDate
            };

            if (!IsValid(userToAdd))
            {
                throw new ArgumentException("The user is invalid!");
            }

            context.Users.Add(userToAdd);
            context.SaveChanges();

            return userToAdd.Id;
        }

        public UserViewModel GetUserById(int id)
        {
            if(!context.Users.Any(u => u.Id == id))
            {
                throw new ArgumentException("No user with that Id!");
            }

            var user = context.Users.Find(id);

            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.RegisterDate
            };
        }

        public void DeleteUser(int userId)
        {
            if (userId < 1 || userId > context.Users.Count())
            {
                throw new ArgumentException("Invalid User Id!");
            }

            var user = context.Users.Find(userId);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
