using System;
using MvcTurbine.ComponentModel;
using DotnetMvcBoilerplate.Core.Provider;
using DotnetMvcBoilerplate.Core.Service;
using DotnetMvcBoilerplate.Core.Security;
using System.Web;
using System.Web.Security;

namespace DotnetMvcBoilerplate.Infrastructure
{
    public class DefaultRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IUserService, UserService>();
            locator.Register<IDatabaseProvider, MongoDatabaseProvider>();
            locator.Register<IEncryption, Encryption>();
            locator.Register<ISessionAuthentication, SessionAuthentication>();
            locator.Register<IHttpContextProvider, HttpContextProvider>();
        }
    }
}