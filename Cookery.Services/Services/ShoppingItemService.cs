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

        public IList<ShoppingItem> GetAllBooks()
        {
            return this.dbContext.ShoppingItems.Where(x => x.ShoppingType == ShoppingType.CookeryBook).ToList();
        }

        public IList<ShoppingItem> GetAllMagazines()
        {
            return this.dbContext.ShoppingItems.Where(x => x.ShoppingType == ShoppingType.CookeryMagazine).ToList();
        }
    }
}
