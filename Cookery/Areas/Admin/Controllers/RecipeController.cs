﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Areas.Admin.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult ReceivedRecipes()
        {
            return View();
        }
    }
}