using AutoMapper;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Services.Services;
using Cookery.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICookeryAccountService accountService;

        public AccountController(IMapper mapper, ICookeryAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }
        
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
                var user = this.mapper.Map<CookeryUser>(model);
                this.accountService.RegisterUser(user, model.Password);
                
                return new RedirectResult("/");
            }
        }
    }
}