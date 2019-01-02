using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Models.Enums;

namespace Cookery.Web.Models.ShoppingItems
{
    public class ShoppingItemViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ShortDescription { get; set; }

        public string Picture { get; set; }

        public ShoppingType ShoppingType { get; set; }

    }
}
