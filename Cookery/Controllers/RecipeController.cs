using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService,
                                 IMapper mapper)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
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

        public IActionResult AllRecipes()
        {
            var allRecipes = this.recipeService.AllPublishedRecipes().ToList();

            var recipeModels = new List<RecipeViewModel>();

            foreach (var recipe in allRecipes)
            {
                var products = recipe.Products.Select(x => new ProductViewModel
                {
                    ProductName = x.Product.ProductName,
                    ProductUnit = x.Product.ProductUnit
                }).ToList();
                
                var recipeModel = this.mapper.Map<RecipeViewModel>(recipe);
                recipeModel.Products = products;

                recipeModels.Add(recipeModel);
            }
            return this.View(recipeModels);
        }

        public IActionResult MyRecipes()
        {
            return this.View();
        }
    }
}