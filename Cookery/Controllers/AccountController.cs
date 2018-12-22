using System.Linq;
using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Services.Services;
using Cookery.Web.Models;
using Cookery.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICookeryAccountService accountService;
        private readonly SignInManager<CookeryUser> signIn;

        public AccountController(IMapper mapper, 
                                 ICookeryAccountService accountService,
                                 SignInManager<CookeryUser> signIn)
        {
            this.mapper = mapper;
            this.accountService = accountService;
            this.signIn = signIn;
        }
        
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            var user = this.signIn.UserManager.Users.FirstOrDefault(u => u.UserName == model.Username);

            var userIsValid = accountService.IsValidUser(user, model.Password);

            if (userIsValid == true) 
            {
                this.signIn.SignInAsync(user, model.RememberMe).Wait();
                ViewBag.Message = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Login. Please, try again!";
                
                return this.View(model);

            }
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
                var user = this.mapper.Map<CookeryUser>(model);
                this.accountService.RegisterUser(user, model.Password);
                
                return new RedirectResult("/");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            foreach (var cookie in this.signIn.Context.Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            this.signIn.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }
    }
}