using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public IActionResult ChangePublishStatus(RecipeViewModel model)
        {
            var recipeId = model.Id;
            var recipe = this.recipeService.GetRecipeById(recipeId);
            
           this.recipeService.UpdatePublishStatus(recipe, model.isPublished);

            if (model.isPublished == true)
            {
                return RedirectToAction("AllRecipes", "Recipe");
            }
            else
            {
                return RedirectToAction("NotPublished", "Recipe", new { area = "Admin" });
            }
        }
    }
}