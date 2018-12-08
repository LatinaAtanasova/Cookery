using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Cookery.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public int Quantity { get; set; }

        public int ShoppingItemId { get; set; }
        public virtual ShoppingItem ShoppingItem { get; set; }


        public string CookeryUserId { get; set; }
        public virtual CookeryUser CookeryUser { get; set; }




    }
}
