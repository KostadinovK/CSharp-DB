using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Models.User
{
    public class UserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
