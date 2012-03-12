using System;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
