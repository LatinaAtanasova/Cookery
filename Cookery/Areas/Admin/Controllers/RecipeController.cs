using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Product;
using Cookery.Web.Models.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Areas.Admin.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult NotPublished()
        {
            var unpublishedRecipes = this.recipeService.UnpublishedRecipes().ToList();

            var recipeModels = new List<RecipeViewModel>();

            foreach (var recipe in unpublishedRecipes)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
       public IActionResult ChangePublishStatus(RecipeViewModel model,string action)
        {
            var recipeId = model.Id;
            var recipe = this.recipeService.GetRecipeById(recipeId);
            RecipeViewModel recipeViewModel = model;

            var actionButton = action;

            if (actionButton == "Edit")
            {
                return RedirectToAction("Edit", "Recipe", new{area="Admin", id=recipeId});
            }

            else 
            {
                this.recipeService.DeleteRecipe(recipeId);
                return RedirectToAction("AllRecipes", "Recipe");
            }


           //this.recipeService.UpdatePublishStatus(recipe, model.isPublished);

           // if (model.isPublished == true)
           // {
           //     return RedirectToAction("AllRecipes", "Recipe");
           // }
           // else
           // {
           //     return RedirectToAction("NotPublished", "Recipe", new { area = "Admin" });
           // }
        }
       [Authorize(Roles = "Admin")]
       [HttpGet]
        public IActionResult Edit(int id)
        {
            var recipeId = id;
            var recipe = recipeService.GetRecipeById(recipeId);

            var recipeModel = this.mapper.Map<RecipeViewModel>(recipe);
            var recipeProductsList = new List<ProductViewModel>();

            foreach (var product in recipe.Products)
            {
                var productModel = new ProductViewModel
                {
                    ProductName = product.Product.ProductName,
                    ProductUnit = product.Product.ProductUnit
                };
                recipeProductsList.Add(productModel);
            }

            recipeModel.Products = recipeProductsList;
            return this.View(recipeModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditRecipe(RecipeViewModel model, int id)
        {
            var modelProducts = model.Products;

            var recipeId = model.Id;
            var recipe = this.recipeService.GetRecipeById(recipeId);

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

            this.recipeService.DeleteProducts(recipe);


            recipe.Products = new List<RecipeProduct>();
            recipe.Products = products;
            recipe.CookeryCategory = model.CookeryCategory;
            recipe.CookingTime = model.CookingTime;
            recipe.Date = DateTime.UtcNow;
            recipe.Description = model.Description;
            recipe.RecipeName = model.RecipeName;
            recipe.isPublished = model.isPublished;

            this.recipeService.EditRecipe(recipe);

            return RedirectToAction("AllRecipes", "Recipe");
        }
    }
}