using Restaurante_APP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurante_APP.Controllers
{
    public class MenuController : Controller
    {
        Restaurante_dbEntities db = new Restaurante_dbEntities();

        public ActionResult Read()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.restaurante_id = new SelectList(db.Restaurante, "restaurante_id", "restaurante_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu prato)
        {
            //if (ModelState.IsValid)
            //{
            //db.Menu.Add(prato);
            //db.SaveChanges();

            ViewBag.Num = prato.preco;
            return View("Read");
            //}

            //ViewBag.restaurante_id = new SelectList(db.Restaurante, "restaurante_id", "restaurante_name");
            //return View();
        }
    }
}