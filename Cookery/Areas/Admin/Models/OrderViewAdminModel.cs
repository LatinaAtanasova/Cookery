using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Models;
using Cookery.Web.Models.ShoppingItems;

namespace Cookery.Web.Areas.Admin.Models
{
    public class OrderViewAdminModel
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public int Quantity { get; set; }

        public ShoppingItemViewModel ShoppingItem { get; set; }

        public string UserName { get; set; }
    }
}
