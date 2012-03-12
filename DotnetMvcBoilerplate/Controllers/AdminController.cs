using System;
using System.Web.Mvc;
using DotnetMvcBoilerplate.Core.Security.Attributes;

namespace DotnetMvcBoilerplate.Controllers
{
    public class AdminController : Controller
    {
        [AdminOnly]
        public ActionResult Index()
        {
            return View();
        }
    }
}
