using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models;

namespace Cookery.Services.Contracts
{
    public interface IRecipeService
    {
        int CreateRecipe(Recipe recipe);

        Recipe GetRecipeById(int id);

        IList<Recipe> AllPublishedRecipes();
    }
}
