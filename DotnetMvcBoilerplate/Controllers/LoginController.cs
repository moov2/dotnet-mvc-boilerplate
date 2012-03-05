using System;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Presents the login for to the user.
        /// </summary>
        /// <returns>Login page.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
