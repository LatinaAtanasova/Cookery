﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cookery.Models
{
    public class Product
    {
        public Product()
        {
            this.RecipesWithProduct = new HashSet<RecipeProduct>();
        }

        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductUnit { get; set; }


        public ICollection<RecipeProduct> RecipesWithProduct { get; set; } // recipes containing this product

    }
}
