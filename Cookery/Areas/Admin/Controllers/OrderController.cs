using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cookery.Services;
using Cookery.Services.Contracts;
using Cookery.Web.Areas.Admin.Models;
using Cookery.Web.Models.Order;
using Cookery.Web.Models.ShoppingItems;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders()
        {
            var allOrders = this.orderService.GetAllOrders();

            var ordersModel = new List<OrderViewAdminModel>();

            foreach (var order in allOrders)
            {
                var orderModel = this.mapper.Map<OrderViewAdminModel>(order);
                var shoppingItem = this.mapper.Map<ShoppingItemViewModel>(order.ShoppingItem);
                var userName = order.CookeryUser.FirstName;
                orderModel.ShoppingItem = shoppingItem;
                orderModel.UserName = userName;
                ordersModel.Add(orderModel);
            }

            return this.View(ordersModel);
        }
    }
}