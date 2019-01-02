using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Web.Models.ShoppingItems;

namespace Cookery.Web.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public int Quantity { get; set; }

        public ShoppingItemViewModel ShoppingItem { get; set; }
    }
}
