using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Error;
using Cookery.Web.Models.Order;
using Cookery.Web.Models.ShoppingItems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderViewModel = Cookery.Web.Models.Order.OrderViewModel;

namespace Cookery.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingItemService shoppingItemService;
        private readonly UserManager<CookeryUser> userManager;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IShoppingItemService shoppingItemService,
                               UserManager<CookeryUser> userManager,
                               IOrderService orderService,
                               IMapper mapper)
        {
            this.shoppingItemService = shoppingItemService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.mapper = mapper;
        }



        public IActionResult AddOrder(ShoppingItemOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shoppingId = model.ShoppingItemId;
                var quantity = model.Quantity;
                var result = this.shoppingItemService.UpdateQuantity(shoppingId, quantity);

                if (result == false)
                {
                    var errorMessageModel = new ErrorMessageViewModel();
                    errorMessageModel.Message = "The available items are less than you are trying to order!";
                    return RedirectToAction("ErrorMessageResult", "Error", errorMessageModel);
                }

                else
                {
                    var currentUser = userManager.GetUserAsync(this.User).Result;
                    var shoppingItem = this.shoppingItemService.GetShoppingItemById(shoppingId);

                    var order = new Order
                    {
                        CookeryUser = currentUser,
                        CookeryUserId = currentUser.Id,
                        OrderedOn = DateTime.Now,
                        Quantity = model.Quantity,
                        ShoppingItem = shoppingItem,
                        ShoppingItemId = shoppingItem.Id
                    };

                    this.orderService.AddOrder(order);
                    return RedirectToAction("MyOrders", "Order");
                }

            }

            return RedirectToAction("All", "ShoppingItem");

        }

        public IActionResult MyOrders()
        {
            var currentUser = userManager.GetUserAsync(this.User).Result;
            var orders = this.orderService.GetOrdersByUser(currentUser).ToList();
            

            var ordersModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var orderModel = this.mapper.Map<OrderViewModel>(order);
                var shoppingItem = this.mapper.Map<ShoppingItemViewModel>(order.ShoppingItem);
                orderModel.ShoppingItem = shoppingItem;
                ordersModel.Add(orderModel);
            }
            

            return this.View(ordersModel);
        }
    }
}