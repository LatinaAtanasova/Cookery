using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models;

namespace Cookery.Services.Contracts
{
    public interface IShoppingItemService
    {
        IList<ShoppingItem> GetAllShoppingItems();

        bool UpdateQuantity(int shoppingId, int quantity);

        ShoppingItem GetShoppingItemById(int shoppingId);
    }
}
