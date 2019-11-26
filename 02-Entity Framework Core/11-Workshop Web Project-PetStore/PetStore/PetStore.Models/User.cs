using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetStore.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
