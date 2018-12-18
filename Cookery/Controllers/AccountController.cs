using Cookery.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class AccountController : Controller
    {
        
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return this.View();
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                return new RedirectResult("/");
            }
        }
    }
}