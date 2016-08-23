using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        public ActionResult Forum()
        {
            this.ViewBag.Message = "Your forum page";

            return this.View();
        }

        public ActionResult Pokédex()
        {
            this.ViewBag.Message = "Your Pokedex page";

            return this.View();
        }

        public ActionResult News()
        {
            this.ViewBag.Message = "Your News page";

            return this.View();
        }
    }
}