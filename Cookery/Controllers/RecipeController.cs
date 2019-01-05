using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Error;
using Cookery.Web.Models.Product;
using Cookery.Web.Models.Recipe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using X.PagedList;

namespace Cookery.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IMapper mapper;
        private readonly UserManager<CookeryUser> userManager;
        private readonly ICookeryAccountService cookeryAccountService;

        public RecipeController(IRecipeService recipeService,
                                IMapper mapper,
                               UserManager<CookeryUser> userManager,
                                ICookeryAccountService cookeryAccountService)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.cookeryAccountService = cookeryAccountService;
        }

        [HttpGet]
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

            var recipe = this.mapper.Map<Recipe>(model);
            recipe.Products = products;

            var recipeId = this.recipeService.CreateRecipe(recipe);

            return RedirectToAction("RecipeDetails", "Recipe", new {id = recipeId});
        }

        public IActionResult RecipeDetails(int id)
        {
            var recipe = this.recipeService.GetRecipeById(id);

            //var recipeModel = new RecipeViewModel
            //{
            //    Id = recipe.Id,
            //    CookeryCategory = recipe.CookeryCategory,
            //    CookingTime = recipe.CookingTime,
            //    Description = recipe.Description,
            //    RecipeName = recipe.RecipeName,
            //    isPublished = recipe.isPublished
            //};

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

            //var products = recipe.Products;
            
            return this.View(recipeModel);
        }

        public IActionResult AllRecipes(int? page)
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

            var pageNumber = page ?? 1;
            var onePageOfRecipes = recipeModels.ToPagedList(pageNumber, 5);

            ViewBag.onePageOfRecipes = onePageOfRecipes;

            return this.View();
        }

        public IActionResult MyRecipes()
        {
            var userId = userManager.GetUserId(this.User);
           
            var currentUser = this.cookeryAccountService.GetUserById(userId);
            var userFavouriteRecipes = currentUser.MyRecipes;

            if (userFavouriteRecipes.Count == 0)
            {
                var errorModel = new ErrorMessageViewModel();
                errorModel.Message = "You don't have favourite recipes!";

                return this.RedirectToAction("ErrorMessageResult", "Error", errorModel);
            }

            else
            {
                var recipeModels = new List<RecipeViewModel>();

                foreach (var recipe in userFavouriteRecipes)
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
        }

        [HttpPost]
        public IActionResult AddToFavourite(RecipeViewModel model)
        {
            var recipe = this.recipeService.GetRecipeById(model.Id);
            var currentUserId = userManager.GetUserId(this.User);
            var currentUser = this.cookeryAccountService.GetUserById(currentUserId);
            cookeryAccountService.AddRecipeAsFavourite(currentUser, recipe);

            return RedirectToAction("MyRecipes", "Recipe", new {id = currentUserId});
        }

        public IActionResult GetRecipesByCategory(string category)
        {
            return this.View();
        }
    }
}