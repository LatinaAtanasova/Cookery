using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Models.Enums;
using Cookery.Web.Models.Product;

namespace Cookery.Web.Models.Recipe
{
    public class RecipeViewModel
    {
        public string RecipeName { get; set; }

        public CookeryCategory CookeryCategory { get; set; }

        public string CookingTime { get; set; } // in minutes

        public string Description { get; set; } // cooking steps

        public List<ProductViewModel> Products { get; set; }
    }
}
