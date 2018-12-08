﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cookery.Models.Enums;

namespace Cookery.Models
{
    public class Recipe
    {
        public Recipe()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; } 

        public string RecipeName { get; set; }

        public CookeryCategory CookeryCategory { get; set; }

        public string CookingTime { get; set; } // in minutes

        public string Description { get; set; } // cooking steps

        public string PhotoUrl { get; set; }  //recipePhoto

        public int? Rating { get; set; } // rating the recipe

        
        public string CookeryUserId { get; set; }
        public virtual CookeryUser CookeryUser { get; set; }


        public virtual ICollection<Product> Products { get; set; }

    }
}
