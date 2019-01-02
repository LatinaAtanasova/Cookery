using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models;

namespace Cookery.Services.Contracts
{
    public interface IOrderService
    {
        void AddOrder(Order newOrder);

        ICollection<Order> GetOrdersByUser(CookeryUser currentUser);
    }
}
