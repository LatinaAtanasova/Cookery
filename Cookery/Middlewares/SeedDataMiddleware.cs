using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Cookery.Models;
using Cookery.Models.Enums;
using Cookery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace Cookery.Web.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context,
                                 CookeryDbContext dbContext,
                                 RoleManager<IdentityRole> roleManager)
        {
            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(roleManager);
            }

            if (!dbContext.ShoppingItems.Any())
            {
                this.SeedShoppingItems(dbContext);
            }
            await this.next(context);
        }

        private void SeedShoppingItems(CookeryDbContext dbContext)
        {
            List<ShoppingItem> shoppingItems = new List<ShoppingItem>();
            shoppingItems.AddRange(new List<ShoppingItem>   
            {
                new ShoppingItem{
                    Author = "Victoria Blashford-Snell",
                    Name = "The Cooking Book",
                    Price = 25.50m,
                    ShortDescription =
                    "The cookbook that really understands what you need in the kitchen, answering all your culinary questions, from what the finished dish should look like and if it can be prepared it ahead, to what to do with leftovers. Over 1, 000 mouth - watering recipes, thousands of explanatory photographs, and superb step - by - step guidance will teach you how to get great home - cooking on the table without fuss.",
                    Picture = "the_cooking_book.png",
                    ShoppingType = ShoppingType.Book
                },
                new ShoppingItem
                {
                    Author = "Sunset Editors",
                    Name = "Sunset Italian cook book",
                    Price = 30,
                    ShortDescription = "Italian cook book",
                    Picture = "italian_cook_book.png",
                    ShoppingType = ShoppingType.Book,
                },
                new ShoppingItem
                {
                    Author = "Nina Timm",
                    Name = "Easy Cooking",
                    Price = 35.50m,
                    ShortDescription = "Easy Cooking from Nina’s Kitchen is a recipe book, yes, but it also reads like a story book, every recipe comes with a memory or story that I have around that particular recipe.",
                    Picture = "Nina_Timm_Easy_Cooking.png",
                    ShoppingType = ShoppingType.Book
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Fine Cooking",
                    Price = 6.35m,
                    ShortDescription = "For those who love to cook!",
                    Picture = "fine_cooking.png",
                    ShoppingType = ShoppingType.Magazine
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Taste of Home",
                    Price = 10,
                    ShortDescription = "In each issue of Taste of Home Magazine, you'll find 100+ family-favorite recipes and tips - recipes from real cooks like you! Enjoy easy, tried-and-proven recipes, everyday ingredients, color photo of every dish, contest winners, 30-minute dishes, mom's best recipes and more.",
                    Picture = "taste_of_home.png",
                    ShoppingType = ShoppingType.Magazine
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Everyday Food",
                    Price = 6.50m,
                    ShortDescription = "Quickly and Delicious",
                    Picture = "everyday-food.png",
                    ShoppingType = ShoppingType.Magazine
                }
            });

            dbContext.ShoppingItems.AddRange(shoppingItems);
            dbContext.SaveChanges();
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRoleResult = await roleManager.CreateAsync(new IdentityRole(CookeryConstants.AdminRole));

            var userRoleResult = await roleManager.CreateAsync(new IdentityRole(CookeryConstants.UserRole));

            if (!adminRoleResult.Succeeded && !userRoleResult.Succeeded)
            {
                throw new ArgumentException("Roles are not created!");
            }
        }
    }
}
