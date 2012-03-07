using System;
using MvcTurbine.ComponentModel;
using DotnetMvcBoilerplate.Core.Provider;
using DotnetMvcBoilerplate.Core.Service;

namespace DotnetMvcBoilerplate.Infrastructure
{
    public class DefaultRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IUserService, UserService>();
            locator.Register<IDatabaseProvider, MongoDatabaseProvider>();
        }
    }
}