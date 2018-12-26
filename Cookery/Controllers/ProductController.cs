using System;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET
        
        public IActionResult ProductPartial(Int32 Index)
        {
            return PartialView(model:Index);
        }

    }
}