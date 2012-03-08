using System;
using MvcTurbine.ComponentModel;
using DotnetMvcBoilerplate.Core.Provider;
using DotnetMvcBoilerplate.Core.Service;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Infrastructure
{
    public class DefaultRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IUserService, UserService>();
            locator.Register<IDatabaseProvider, MongoDatabaseProvider>();
            locator.Register<IEncryption, Encryption>();
        }
    }
}