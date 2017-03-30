using Restaurante_APP.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            return View("Create");
        }

        public ActionResult Read()
        {
            ViewBag.str_search = "Pesquisar";
            return View(db.Restaurante.OrderBy(q => q.restaurante_name));
        }

        [HttpPost]
        public ActionResult Read(String r_name)
        {
            string regex_space = @"[\ ]+";
            Regex r = new Regex(regex_space, RegexOptions.IgnoreCase);
            Match m = r.Match(r_name);
            if (r_name.Equals("") | m.Success)
            {
                ViewBag.str_search = "Pesquisar";
            }
            else
            {
                ViewBag.str_search = r_name;
            }
            var restaurante = db.Restaurante.OrderBy(q => q.restaurante_name).
                                    Where(q => q.restaurante_name.Contains(r_name));
            return View(restaurante);
        }

        public ActionResult Update(int? id)
        {
            if(id == null)
            {
                ViewBag.MsgUpdate = "ID vazio";
                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            Restaurante r_update = db.Restaurante.Find(id);
            if (r_update == null)
            {
                ViewBag.MsgUpdate = "ID não encontrado";
                ViewBag.str_search = "Pesquisar";
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

                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            ViewBag.str_search = "Pesquisar";
            return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                ViewBag.MsgUpdate = "ID Vazio";
                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            Restaurante r_delete = db.Restaurante.Find(id);
            if (r_delete == null)
            {
                ViewBag.MsgUpdate = "ID não encotrado.";
                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }
            db.Restaurante.Remove(r_delete);

            var p_delete = db.Menu.Where(q => q.restaurante_id == id);
            if (p_delete == null)
            {
                db.SaveChanges();
                ViewBag.MsgUpdate = "Restaurante sem pratos cadastrados";
                ViewBag.str_search = "Pesquisar";
                return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
            }

            db.Menu.RemoveRange(p_delete);
            db.SaveChanges();
            ViewBag.MsgUpdate = "Os pratos ligados a esse restaurante também foram deletados.";
            ViewBag.str_search = "Pesquisar";
            return View("Read", db.Restaurante.OrderBy(q => q.restaurante_name));
        }
    }
}