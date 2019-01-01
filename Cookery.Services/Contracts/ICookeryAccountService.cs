using Cookery.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Cookery.Services.Contracts
{
    public interface ICookeryAccountService
    {
       void RegisterUser(CookeryUser cookeryUser, string userPassword);

        bool IsValidUser(CookeryUser cookeryUser, string userPassword);

        CookeryUser GetUserById(string userId);

        void AddRecipeAsFavourite(CookeryUser user, Recipe recipe);
    }
}
