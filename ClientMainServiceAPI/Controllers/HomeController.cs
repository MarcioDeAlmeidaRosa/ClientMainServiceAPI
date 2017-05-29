using System.Web.Mvc;

namespace ClientMainServiceAPI.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return RedirectToAction("Index", "Help");
        }
    }
}
