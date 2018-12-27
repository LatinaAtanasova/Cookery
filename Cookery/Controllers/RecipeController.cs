using System.Collections.Generic;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Product;
using Cookery.Web.Models.Recipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace Cookery.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        // GET
        public IActionResult CreateRecipe()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRecipe(RecipeViewModel model)
        {
            var modelProducts = model.Products;
            var products = new List<RecipeProduct>();

            foreach (var modelProduct in modelProducts)
            {
                if (modelProduct.ProductName == null || modelProduct.ProductUnit == null)
                {
                    break;
                }
                else
                {
                    var recipeProduct = new RecipeProduct
                    {
                        Product = new Product
                        {
                            ProductName = modelProduct.ProductName,
                            ProductUnit = modelProduct.ProductUnit
                        },
                    };
                    products.Add(recipeProduct);
                }
            }


            var recipe = new Recipe
            {
                RecipeName = model.RecipeName,
                CookeryCategory = model.CookeryCategory,
                CookingTime = model.CookingTime,
                Description = model.Description,
                Products = products

            };
            var recipeId = this.recipeService.CreateRecipe(recipe);

            return RedirectToAction("RecipeDetails", "Recipe", recipeId);
        }

        public IActionResult RecipeDetails(int id)
        {
            var recipe = this.recipeService.GetRecipeById(id);

            var recipeModel = new RecipeViewModel
            {
                CookeryCategory = recipe.CookeryCategory,
                CookingTime = recipe.CookingTime,
                Description = recipe.Description,
                RecipeName = recipe.RecipeName
            };

            //var products = recipe.Products;
            return this.View(recipeModel);
        }
    }
}