using System.Collections.Generic;
using AutoMapper;
using Cookery.Services.Contracts;
using Cookery.Web.Models.ShoppingItems;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ShoppingItemController : Controller
    {
        private readonly IShoppingItemService shoppingItemService;
        private readonly IMapper mapper;

        public ShoppingItemController(IShoppingItemService shoppingItemService,
                                      IMapper mapper)
        {
            this.shoppingItemService = shoppingItemService;
            this.mapper = mapper;
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