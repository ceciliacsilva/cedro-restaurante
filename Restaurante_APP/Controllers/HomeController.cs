using System.Web.Mvc;

namespace Restaurante_APP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Read()
        {
            return View();
        }
    }
}