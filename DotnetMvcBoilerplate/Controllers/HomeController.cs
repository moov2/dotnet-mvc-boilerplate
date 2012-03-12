using System;
using System.Web.Mvc;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Controllers
{
    public class HomeController : Controller
    {
        private ISessionAuthentication _sessionAuthentication;

        public HomeController(ISessionAuthentication sessionAuthentication)
        {
            _sessionAuthentication = sessionAuthentication;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>Home page.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Ends the authenticate session thus signing
        /// the user out of their account.
        /// </summary>
        /// <returns>Redirected to the login form.</returns>
        public ActionResult Logout()
        {
            _sessionAuthentication.End();
            return Redirect("/Login");
        }
    }
}
