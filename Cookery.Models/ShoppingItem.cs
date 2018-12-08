using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models.Enums;

namespace Cookery.Models
{
   public class ShoppingItem
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public ShoppingType ShoppingType { get; set; }

        public string CookeryUserId { get; set; }
        public virtual CookeryUser User { get; set; }
    }
}
