using System;
using System.Web.Mvc;
using DotnetMvcBoilerplate.ViewModels.Login;

namespace DotnetMvcBoilerplate.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Presents the login for to the user.
        /// </summary>
        /// <returns>Login page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Attempts to authenticate credentials.
        /// </summary>
        /// <param name="model">Credentials entered by a user.</param>
        /// <returns>Redirects the user to the home page if successful, 
        /// otherwise they are returned to the login form and given 
        /// failure feedback.</returns>
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            throw (new NotImplementedException());
        }
    }
}
