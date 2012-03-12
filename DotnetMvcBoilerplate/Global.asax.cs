using System;
using MvcTurbine.Web;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using DotnetMvcBoilerplate.Core.IO;
using DotnetMvcBoilerplate.Core.Security;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Service;
using System.Web;

namespace DotnetMvcBoilerplate
{
    public class MvcApplication : TurbineApplication
    {
        /// <summary>
        /// Flag indicating if the user is logged in.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return Context.User != null && Context.User.Identity.IsAuthenticated; }
        }

        public MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
            this.AuthenticateRequest += new EventHandler(Application_AuthenticateRequest);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (IsAuthenticated)
                ServiceLocator.Resolve<ISessionAuthentication>().SetRoles();
        }

        /// <summary>
        /// Gets the User object from the database.
        /// </summary>
        /// <returns>User that is logged in.</returns>
        private User GetLoggedInUser()
        {
            var userService = ServiceLocator.Resolve<IUserService>();
            return userService.ById(HttpContext.Current.User.Identity.Name);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            if (IsAuthenticated)
                ServiceLocator.Resolve<SessionAuthentication>().SetSessionData(GetLoggedInUser()); 
        }

        public override void Startup()
        {
            WindowsService.StartIfNotRunning(WindowsService.MongoDB);
        }
    }
}