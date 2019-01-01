using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Models.Enums;
using Cookery.Web.Models.Product;

namespace Cookery.Web.Models.Recipe
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        public string RecipeName { get; set; }

        public CookeryCategory CookeryCategory { get; set; }

        public string CookingTime { get; set; } // in minutes

        public string Description { get; set; } // cooking steps

        public string PhotoUrl { get; set; }

        public bool isPublished { get; set; }

  public DateTime Date = DateTime.Now;

        public List<ProductViewModel> Products { get; set; }

    }
}
