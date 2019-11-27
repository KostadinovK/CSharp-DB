using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetStore.Data;
using PetStore.Models.Enums;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Order;

namespace PetStore.Services.Implementations
{
    public class OrderService : Service, IOrderService
    {
        public OrderService(PetStoreDbContext context) : base(context)
        {
        }

        public void CompleteOrder(int id)
        {
            if (id < 1 || id > context.Orders.Count())
            {
                throw new ArgumentException("Invalid Id!");
            }

            var order = context.Orders.Find(id);
            order.OrderStatus = OrderStatus.Completed;
            context.SaveChanges();
        }

        public void CancelOrder(int id)
        {
            if (id < 1 || id > context.Orders.Count())
            {
                throw new ArgumentException("Invalid Order Id!");
            }

            var order = context.Orders.Find(id);
            order.OrderStatus = OrderStatus.Canceled;
            context.SaveChanges();
        }

        public IEnumerable<OrderListingModel> GetAllOrdersByUserId(int userId)
        {
            if (userId < 1 || userId > context.Users.Count())
            {
                throw new ArgumentException("Invalid User Id!");
            }

            return context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderListingModel()
                {
                    UserId = o.UserId,
                    Description = o.Description,
                    DateTime = o.DateTime,
                    OrderStatus = o.OrderStatus
                })
                .ToList();
        }

        public IEnumerable<OrderListingModel> GetAllOrders()
        {
            return context.Orders
                .Select(o => new OrderListingModel()
                {
                    UserId = o.UserId,
                    Description = o.Description,
                    DateTime = o.DateTime,
                    OrderStatus = o.OrderStatus
                })
                .ToList();
        }
    }
}
