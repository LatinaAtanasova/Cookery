using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Contracts;

namespace Cookery.Services.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly CookeryDbContext dbContext;

        public RecipeService(CookeryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public int CreateRecipe(Recipe recipe)
        {
            this.dbContext.Recipes.Add(recipe);
            dbContext.SaveChanges();

            return recipe.Id;
        }

        public Recipe GetRecipeById(int id)
        {
            var recipe = dbContext.Recipes.FirstOrDefault(x => x.Id == id);
            return recipe;
        }

        public IList<Recipe> AllPublishedRecipes()
        {
            var allPublishedRecipes = this.dbContext.Recipes
                .Where(x => x.isPublished == true)
                .Include(recipe => recipe.Products)
                .ToList();

            return allPublishedRecipes;
        }

        public IList<Recipe> UnpublishedRecipes()
        {
            var unpublishedRecipes = this.dbContext.Recipes
                .Where(x => x.isPublished == false)
                .Include(recipe => recipe.Products)
                .ToList();

            return unpublishedRecipes;
        }

        public void UpdatePublishStatus(Recipe recipe, bool newstatus)
        {
            recipe.isPublished = newstatus;
            dbContext.SaveChanges();
        }

        public void DeleteRecipe(int id)
        {
            var recipe = dbContext.Recipes.FirstOrDefault(x => x.Id == id);

            var recipeProducts = recipe.Products;
            foreach (var recipeProduct in recipeProducts.ToList())
            {
                var products = recipeProducts.Select(x => x.Product).ToList();
                ;
                foreach (var product in products.ToList())
                {
                    dbContext.Products.Remove(product);
                }
            }
            dbContext.Recipes.Remove(recipe);
            dbContext.SaveChanges();
        }

        public void EditRecipe(Recipe recipe)
        {
            dbContext.SaveChanges();
        }

        public void DeleteProducts(Recipe recipe)
        {
            var recipeProducts = recipe.Products;
            foreach (var recipeProduct in recipeProducts.ToList())
            {
                var products = recipeProducts.Select(x => x.Product).ToList();
                ;
                foreach (var product in products.ToList())
                {
                    dbContext.Products.Remove(product);
                }

                dbContext.SaveChanges();
            }
        }
    }
}