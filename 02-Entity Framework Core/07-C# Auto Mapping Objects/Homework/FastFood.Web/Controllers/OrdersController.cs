using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;

namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Name).ToList(),
                Employees = this.context.Employees.Select(x => x.Name).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var order = mapper.Map<Order>(model);
            order.DateTime = DateTime.Now;
            Enum.TryParse(model.OrderType, out OrderType orderType);
            order.Type = orderType;

            var item = context.Items.FirstOrDefault(i => i.Name == model.ItemName);
            var employee = context.Employees.FirstOrDefault(e => e.Name == model.EmployeeName);

            order.Employee = employee;
            order.OrderItems.Add(new OrderItem{
                Order = order,
                ItemId = item.Id,
                Quantity = model.Quantity
            });

            context.Orders.Add(order);
            context.SaveChanges();

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = context.Orders
                .ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
