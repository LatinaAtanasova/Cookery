using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ShoppingItemController : Controller
    {
        // GET
        public IActionResult AllBooks()
        {
            return this.View();
        }

        public IActionResult AllMagazines()
        {
            return this.View();
        }
    }
}