using System;
using System.Collections.Generic;
using System.Text;

namespace Cookery.Models
{
    public class RecipeProduct
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
