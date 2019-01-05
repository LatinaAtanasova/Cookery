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
    public class CookeryShoppingItemServiceTest
    {
        [Fact]
        public void ReturnAllShoppingItems()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                dbContext.ShoppingItems.AddRange(GetShoppingItemsSample());
                dbContext.SaveChanges();

                var service = new ShoppingItemService(dbContext);
                count = service.GetAllShoppingItems().Count;

            }
            Assert.Equal(2, count);
        }

        private ShoppingItem[] GetShoppingItemsSample()
        {
            ShoppingItem[] sample =
            {
                new ShoppingItem
                {
                    Author = "Karl",
                    Name = "Cook or not to cook",
                    Price = 25,
                    Quantity = 10,
                    ShoppingType = ShoppingType.Book,
                    ShortDescription = "something funny here"
                },
                new ShoppingItem
                {
                    Author = "Mark Carol",
                    Name = "Cooking in the rain",
                    Price = 30,
                    Quantity = 15,
                    ShoppingType = ShoppingType.Magazine,
                    ShortDescription = "something funny here"
                }
            };

            return sample;
        }

        [Fact]
        public void GetShoppingItemById()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            ShoppingItem result = null;

            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                dbContext.ShoppingItems.AddRange(GetShoppingItemsSample());
                dbContext.SaveChanges();

                var service = new ShoppingItemService(dbContext);
                result = service.GetShoppingItemById(1);

            }
            Assert.NotNull(result);


        }

        [Fact]
        public void UpdateQuantityShoppingItem()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            bool result = false;

            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var item = dbContext.ShoppingItems.FirstOrDefault();
                item.Quantity = 7;
                var id = item.Id;
                
                dbContext.SaveChanges();

                var service = new ShoppingItemService(dbContext);
                result = service.UpdateQuantity(id, 5);

            }
            Assert.True(result);
        }
    }
}
