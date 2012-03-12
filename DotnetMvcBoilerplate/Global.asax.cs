using System;
using MvcTurbine.Web;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using DotnetMvcBoilerplate.Core.IO;
using DotnetMvcBoilerplate.Core.Security;

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

        public override void Startup()
        {
            WindowsService.StartIfNotRunning(WindowsService.MongoDB);
        }
    }
}