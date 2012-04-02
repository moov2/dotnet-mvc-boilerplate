using System;
using MvcTurbine.Web;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using DotnetMvcBoilerplate.Core.IO;
using DotnetMvcBoilerplate.Core.Security;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Service;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotnetMvcBoilerplate.Controllers;

namespace DotnetMvcBoilerplate
{
    public class MvcApplication : TurbineApplication
    {
        private const string MongoNotInstalledUrl = "/MongoNotInstalled";

        /// <summary>
        /// Flag indicating if the user is logged in.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return Context.User != null && Context.User.Identity.IsAuthenticated; }
        }

        /// <summary>
        /// Flag indicating whether MongoDB is installed
        /// as a Windows Service on the server machine.
        /// </summary>
        public bool MongoInstalled
        {
            get { return (bool)Application["MongoInstalled"]; }
            set { Application["MongoInstalled"] = value; }
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

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (!MongoInstalled && !NavigatingToMongoNotInstalled() && IsPageRequest())
                Response.Redirect(MongoNotInstalledUrl);
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

        /// <summary>
        /// Returns flag indicating whether the request is for
        /// a web page.
        /// </summary>
        /// <returns>True if Request is for a page, otherwise false.</returns>
        private bool IsPageRequest()
        {
            return Request.CurrentExecutionFilePathExtension == "";
        }

        /// <summary>
        /// Returns true if the Request is to the MongoNotInstalled
        /// page, otherwise false will be returned.
        /// </summary>
        /// <returns>True if navigation to mongo not install
        /// page, otherwise false.</returns>
        private bool NavigatingToMongoNotInstalled()
        {
            return Request.Url.LocalPath == MongoNotInstalledUrl;
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            if (IsAuthenticated)
                ServiceLocator.Resolve<SessionAuthentication>().SetSessionData(GetLoggedInUser()); 
        }

        public override void Startup()
        {
            MongoInstalled = WindowsService.StartIfNotRunning(WindowsService.MongoDB);
        }
    }
}