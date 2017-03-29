using Restaurante_APP.Models;
using System.Collections.Generic;
using System.Linq;
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
            //return View("Read-id", db.Menu.OrderBy(q => q.prato_name));
        }

        public ActionResult Create()
        {
            var restaurante_list = new SelectList(db.Restaurante, "restaurante_id", "restaurante_name");
            ViewBag.restaurante_id = restaurante_list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu prato)
        {
            if (ModelState.IsValid && !(db.Restaurante.Find(prato.restaurante_id) == null))
            {
                db.Menu.Add(prato);
                db.SaveChanges();

                return View("Read-name", Pratos_Restaurante());
            }

            ViewBag.MsgID = "Cadastre um restaurante para cadastrar o prato.";
            var restaurante_list = new SelectList(db.Restaurante, "restaurante_id", "restaurante_name");
            ViewBag.restaurante_id = restaurante_list;
            return View();
        }

        public ActionResult Update(int? id)
        {
            if(id == null)
            {
                ViewBag.MsgUpdate = "ID Vazio.";
                return View("Read-name", Pratos_Restaurante());
            }

            var p_update = db.Menu.Find(id);
            if (p_update == null)
            {
                ViewBag.MsgUpdate = "ID (prato) não encontrado.";
                return View("Read-name", Pratos_Restaurante());
            }
            var restaurante_list = new SelectList(db.Restaurante,
                 "restaurante_id", "restaurante_name", p_update.restaurante_id);
            ViewBag.restaurante_id = restaurante_list;
            return View(p_update);
        }

        [HttpPost]
        public ActionResult Update(Menu prato)
        {
            var p_update = db.Menu.Find(prato.prato_id);
            p_update.restaurante_id = prato.restaurante_id;
            p_update.prato_name = prato.prato_name;
            p_update.preco = prato.preco;
            db.SaveChanges();

            return View("Read-name", Pratos_Restaurante());
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
