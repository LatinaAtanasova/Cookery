using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult AllOrders()
        {
            return this.View();
        }
    }
}