using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
