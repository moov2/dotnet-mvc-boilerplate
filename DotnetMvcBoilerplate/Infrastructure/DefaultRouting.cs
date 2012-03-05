using System;
using MvcTurbine.Routing;
using System.Web.Routing;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Infrastructure
{
    public class DefaultRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}