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

        IList<Recipe> UnpublishedRecipes();

        void UpdatePublishStatus(Recipe recipe, bool newstatus);

        void DeleteRecipe(int id);

        void EditRecipe(Recipe recipe);

        void DeleteProducts(Recipe recipe);
    }
}
