using System;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class AuthorisedController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
