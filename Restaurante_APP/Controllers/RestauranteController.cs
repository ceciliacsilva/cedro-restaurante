﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurante_APP.Controllers
{
    public class RestauranteController : Controller
    {
        // GET: Restaurante
        public ActionResult Index()
        {
            return View();
        }
    }
}