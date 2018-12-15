using System;
using System.Collections.Generic;
using System.Text;

namespace Cookery.Models
{
    public class RecipeProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
