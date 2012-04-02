using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class MongoNotInstalledController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
