using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cookery.Services.Services
{
    public class CookeryAccountService : ICookeryAccountService
    {
        private readonly SignInManager<CookeryUser> signIn;
        private readonly UserManager<CookeryUser> userManager;
        private readonly CookeryDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public CookeryAccountService(SignInManager<CookeryUser> signIn, 
                                    UserManager<CookeryUser> userManager,
                                    CookeryDbContext dbContext,
                                    RoleManager<IdentityRole> roleManager)
        {
            this.signIn = signIn;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }

        public void RegisterUser(CookeryUser cookeryUser, string userPassword)
        {
            var createUserResult = this.signIn.UserManager
                                            .CreateAsync(cookeryUser, userPassword).Result;

            if (createUserResult.Succeeded)
            {
                if (dbContext.Users.Count() == 1)
                {
                    var userRoleResult = this.userManager.AddToRoleAsync(cookeryUser, CookeryConstants.AdminRole).Result;

                    if (!userRoleResult.Succeeded)
                    {
                        throw new ArgumentException("Role was not assigned to user!");
                    }
                }
                else if (dbContext.Users.Count() > 1)
                {
                    var userRoleResult = this.userManager.AddToRoleAsync(cookeryUser, CookeryConstants.UserRole).Result;

                    if (!userRoleResult.Succeeded)
                    {
                        throw new ArgumentException("Role was not assigned to user!");
                    }
                }
            }
        }

        public bool IsValidUser(CookeryUser cookeryUser, string userPassword)
        {
            var isValid = this.signIn.UserManager.CheckPasswordAsync(cookeryUser, userPassword).Result;
            if (isValid.Equals(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
