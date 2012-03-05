using System;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Controllers
{
    public class InstallController : Controller
    {
        /// <summary>
        /// Displays the Install form that allows the
        /// user to create the initial admin account.
        /// </summary>
        /// <returns>Form for creating initial admin.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
