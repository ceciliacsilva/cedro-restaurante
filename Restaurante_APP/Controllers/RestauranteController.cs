using Restaurante_APP.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Restaurante_APP.Controllers
{
    public class RestauranteController : Controller
    {
        Restaurante_dbEntities db = new Restaurante_dbEntities();

        //Create default
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Restaurante.Add(restaurante);
                db.SaveChanges();

                ViewBag.MsgUpdate = "Restaurante criado com sucesso.";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            return View("Create");
        }

        public ActionResult Read()
        {
            return View(db.Restaurante.OrderBy(q => q.restaurante_name));
        }

        [HttpPost]
        public ActionResult Read(String r_name)
        {
            if (r_name.Equals(""))
            {
                return View(db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            //var restaurante = db.Restaurante.OrderBy(q => q.restaurante_name).Where(q => q.restaurante_name == r_name); 
            var restaurante = from q_res in db.Restaurante
                              where q_res.restaurante_name == r_name
                              select q_res;
            return View(restaurante);
        }

        public ActionResult Update(int? id)
        {
            if(id == null)
            {
                ViewBag.MsgUpdate = "ID vazio";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            Restaurante r_update = db.Restaurante.Find(id);
            if (r_update == null)
            {
                ViewBag.MsgUpdate = "ID não encontrado";
                return View("Read",db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            return View(r_update);
        }

        [HttpPost]
        public ActionResult Update(Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                var r_update = db.Restaurante.Find(restaurante.restaurante_id);
                r_update.restaurante_name = restaurante.restaurante_name;
                db.SaveChanges();

                ViewBag.MsgUpdate = "Atualizado com sucesso.";

                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                ViewBag.MsgUpdate = "ID Vazio";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            Restaurante r_delete = db.Restaurante.Find(id);
            if (r_delete == null)
            {
                ViewBag.MsgUpdate = "ID não encotrado.";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            db.Restaurante.Remove(r_delete);

            var p_delete = db.Menu.Where(q => q.restaurante_id == id);
            if (p_delete == null)
            {
                db.SaveChanges();
                ViewBag.MsgUpdate = "Restaurante sem pratos cadastrados";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            db.Menu.RemoveRange(p_delete);
            db.SaveChanges();
            ViewBag.MsgUpdate = "Os pratos ligados a esse restaurante também foram deletados.";
            return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
        }
    }
}