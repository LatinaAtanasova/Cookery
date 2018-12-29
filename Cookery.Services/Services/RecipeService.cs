using System;
using System.Collections.Generic;
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

        public ICollection<Recipe> AllPublishedRecipes()
        {
            var allPublishedRecipes = this.dbContext.Recipes.Where(x => x.isPublished == true).ToList();
            return allPublishedRecipes;
        }
    }
}
