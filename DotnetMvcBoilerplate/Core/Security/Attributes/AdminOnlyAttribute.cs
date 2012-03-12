using System;
using System.Web.Mvc;
using System.Web;
using DotnetMvcBoilerplate.Models;

namespace DotnetMvcBoilerplate.Core.Security.Attributes
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            if (!context.User.Identity.IsAuthenticated)
                return false;

            return context.User.IsInRole(Role.Admin);
        }
    }
}