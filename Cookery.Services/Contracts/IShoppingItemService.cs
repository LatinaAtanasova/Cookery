using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models;

namespace Cookery.Services.Contracts
{
    public interface IShoppingItemService
    {
        IList<ShoppingItem> GetAllShoppingItems();

        
    }
}
