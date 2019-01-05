using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookery.Web.Models.ShoppingItems
{
    public class ShoppingItemOrderViewModel
    {

        public int ShoppingItemId { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

    }
}
