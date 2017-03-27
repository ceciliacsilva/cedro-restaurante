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

        public IEnumerable<Menu_view> Pratos_Restaurante()
        {
            var pratos = (from prato in db.Menu
                          join restaurante in db.Restaurante on prato.restaurante_id equals restaurante.restaurante_id
                          select new Menu_view
                          {
                              prato_id = prato.prato_id,
                              restaurante_name = restaurante.restaurante_name,
                              prato_name = prato.prato_name,
                              preco = prato.preco
                          });

            return pratos;
        }

        public ActionResult Read()
        {
            return View("Read-name", Pratos_Restaurante());
            //return View("Read", db.Menu.OrderBy(q => q.prato_name));
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
            if (ModelState.IsValid)
            {
                db.Menu.Add(prato);
                db.SaveChanges();

                return View("Read-name", Pratos_Restaurante());
            }

            ViewBag.restaurante_id = new SelectList(db.Restaurante, "restaurante_id", "restaurante_name");
            return View();
        }

        public ActionResult Update(int? id)
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ViewBag.MsgUpdate = "ID (prato) vazio.";
                return View("Read-name", Pratos_Restaurante());
            }
            Menu p_delete = db.Menu.Find(id);
            if(p_delete == null)
            {
                ViewBag.MsgUpdate = "ID (prato) não encontrado.";
                return View("Read-name", Pratos_Restaurante());
            }

            db.Menu.Remove(p_delete);
            db.SaveChanges();
            return View("Read-name", Pratos_Restaurante());
        }
    }
}
/*
      double aa;
      NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("fr-FR");
            Double.TryParse("52,8725945", style, culture, out aa);
            //r_preco = r_preco.Replace(",", ".");
            //ViewBag.Num = r_preco;
            //aa = Convert.ToDouble(r);
            //prato.preco = aa;
            aa = aa * 1;
            prato.preco = (double) Convert.ToDecimal(3.4) ;
 */
