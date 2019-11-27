using System;
using System.Collections.Generic;
using System.Text;
using PetStore.Services.Models.Order;

namespace PetStore.Services.Interfaces
{
    public interface IOrderService
    {
        void CompleteOrder(int id);

        void CancelOrder(int id);

        IEnumerable<OrderListingModel> GetAllOrdersByUserId(int userId);

        IEnumerable<OrderListingModel> GetAllOrders();
    }
}
