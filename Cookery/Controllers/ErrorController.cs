using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Web.Models.Error;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorMessageResult(ErrorMessageViewModel model)
        {
            return this.View(model);
        }
    }
}