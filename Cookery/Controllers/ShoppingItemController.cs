using System.Collections.Generic;
using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Error;
using Cookery.Web.Models.ShoppingItems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ShoppingItemController : Controller
    {
        private readonly IShoppingItemService shoppingItemService;
        private readonly IMapper mapper;
        private readonly UserManager<CookeryUser> userManager;
        private readonly ICookeryAccountService cookeryAccountService;


        public ShoppingItemController(IShoppingItemService shoppingItemService,
                                      IMapper mapper,
                                      UserManager<CookeryUser> userManager,
                                      ICookeryAccountService cookeryAccountService)
        {
            this.shoppingItemService = shoppingItemService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.cookeryAccountService = cookeryAccountService;
        }
        // GET
        public IActionResult All()
        {
            var shoppingItemList = shoppingItemService.GetAllShoppingItems();
            var shoppingItemCollection = new List<ShoppingItemViewModel>();

            foreach (var item in shoppingItemList)
            {
                var shoppingItem = this.mapper.Map<ShoppingItemViewModel>(item);
                shoppingItemCollection.Add(shoppingItem);
            }

            return this.View(shoppingItemCollection);
        }



    }
}