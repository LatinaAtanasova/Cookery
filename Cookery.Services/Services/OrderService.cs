using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Contracts;

namespace Cookery.Services.Services
{
   public class OrderService : IOrderService
   {
       private readonly CookeryDbContext dbContext;

       public OrderService(CookeryDbContext dbContext)
       {
           this.dbContext = dbContext;
       }



        public void AddOrder(Order newOrder)
        {
            this.dbContext.Orders.Add(newOrder);
            dbContext.SaveChanges();
        }

        public ICollection<Order> GetOrdersByUser(CookeryUser currentUser)
        {
            var orders = dbContext.Orders.Where(x => x.CookeryUser == currentUser).Include(x => x.ShoppingItem).ToList();
            return orders;
        }
    }
}
