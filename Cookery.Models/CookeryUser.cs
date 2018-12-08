using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Cookery.Models
{
    public class CookeryUser : IdentityUser
    {
        public CookeryUser()
        {
            this.MyRecipes = new HashSet<Recipe>();
            this.ShoppingItems = new HashSet<ShoppingItem>();
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }


        public virtual ICollection<Recipe> MyRecipes { get; set; }
        public virtual ICollection<ShoppingItem> ShoppingItems { get; set; }

    }
}
