using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Models.Enums;
using Cookery.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cookery.Tests
{
    public class CookeryOrderServiceTest
    {
        [Fact]
        public void AddOrder()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var order = new Order
                {
                    ShoppingItem = new ShoppingItem
                    {
                        Author = "Karl",
                        Name = "Cook or not to cook",
                        Price = 25,
                        Quantity = 10,
                        ShoppingType = ShoppingType.Book,
                        ShortDescription = "something funny here"
                    },
                    Quantity = 2,
                    CookeryUser = new CookeryUser
                    {
                        Gender = Gender.Male,
                        Email = "martin@abv.bg",
                        FirstName = "Marin",
                        LastName = "Velikov"
                    },
                    OrderedOn = DateTime.UtcNow,
                };
                  
                var service = new OrderService(dbContext);
                service.AddOrder(order);
                count = dbContext.Orders.Count();

            }
            Assert.Equal(1, count);
        }

        [Fact]
        public void GetOrdersByUser()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var result = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var order = new Order
                {
                    ShoppingItem = new ShoppingItem
                    {
                        Author = "Karl",
                        Name = "Cook or not to cook",
                        Price = 25,
                        Quantity = 10,
                        ShoppingType = ShoppingType.Book,
                        ShortDescription = "something funny here"
                    },
                    Quantity = 2,
                    CookeryUser = new CookeryUser
                    {
                        Gender = Gender.Male,
                        Email = "martin@abv.bg",
                        FirstName = "Marin",
                        LastName = "Velikov"
                    },
                    OrderedOn = DateTime.UtcNow,
                };

                var service = new OrderService(dbContext);
                service.AddOrder(order);
                var user = order.CookeryUser;
                result = service.GetOrdersByUser(user).Count;

            }
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllOrders()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var result = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var order = new Order
                {
                    ShoppingItem = new ShoppingItem
                    {
                        Author = "Karl",
                        Name = "Cook or not to cook",
                        Price = 25,
                        Quantity = 10,
                        ShoppingType = ShoppingType.Book,
                        ShortDescription = "something funny here"
                    },
                    Quantity = 2,
                    CookeryUser = new CookeryUser
                    {
                        Gender = Gender.Male,
                        Email = "martin@abv.bg",
                        FirstName = "Marin",
                        LastName = "Velikov"
                    },
                    OrderedOn = DateTime.UtcNow,
                };
                var order2 = new Order
                {
                    ShoppingItem = new ShoppingItem
                    {
                        Author = "Karl",
                        Name = "Cook or not to cook",
                        Price = 25,
                        Quantity = 10,
                        ShoppingType = ShoppingType.Book,
                        ShortDescription = "something funny here"
                    },
                    Quantity = 2,
                    CookeryUser = new CookeryUser
                    {
                        Gender = Gender.Male,
                        Email = "martin@abv.bg",
                        FirstName = "Marin",
                        LastName = "Velikov"
                    },
                    OrderedOn = DateTime.UtcNow,
                };

                var service = new OrderService(dbContext);
                dbContext.Orders.AddRange(order, order2);
                dbContext.SaveChanges();
                result = service.GetAllOrders().Count;

            }
            Assert.Equal(2, result);
        }
    }
}
