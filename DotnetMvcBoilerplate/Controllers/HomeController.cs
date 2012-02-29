using System;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
