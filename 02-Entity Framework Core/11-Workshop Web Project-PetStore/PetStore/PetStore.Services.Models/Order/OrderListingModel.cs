using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.Services.Models.User;


namespace PetStore.Services.Models.Order
{
    public class OrderListingModel
    {
        public int UserId { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
