using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace Cookery.Web.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context,
                                 CookeryDbContext dbContext,
                                 RoleManager<IdentityRole> roleManager)
        {
            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(roleManager);
            }

            await this.next(context);
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRoleResult = await roleManager.CreateAsync(new IdentityRole(Constants.AdminRole));

            var userRoleResult = await roleManager.CreateAsync(new IdentityRole(Constants.UserRole));

            if (!adminRoleResult.Succeeded && !userRoleResult.Succeeded)
            {
                throw new ArgumentException("Roles are not created!");
            }
        }
    }
}
