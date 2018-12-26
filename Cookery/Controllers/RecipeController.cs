using Cookery.Web.Models.Recipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace Cookery.Web.Controllers
{
    public class RecipeController : Controller
    {
        // GET
        public IActionResult CreateRecipe()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRecipe(RecipeViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}