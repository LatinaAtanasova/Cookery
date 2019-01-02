using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Models.Enums;
using Cookery.Services.Contracts;

namespace Cookery.Services.Services
{
    public class ShoppingItemService : IShoppingItemService
    {
        private readonly CookeryDbContext dbContext;

        public ShoppingItemService(CookeryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<ShoppingItem> GetAllShoppingItems()
        {
            return this.dbContext.ShoppingItems.ToList();
        }

        public bool UpdateQuantity(int shoppingId, int quantity)
        {
            var shoppingItem = GetShoppingItemById(shoppingId);
            if (shoppingItem.Quantity >= quantity)
            {
                shoppingItem.Quantity -= quantity;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShoppingItem GetShoppingItemById(int shoppingId)
        {
            var shoppingItem = dbContext.ShoppingItems.FirstOrDefault(x => x.Id == shoppingId);
            return shoppingItem;
        }
    }
}
