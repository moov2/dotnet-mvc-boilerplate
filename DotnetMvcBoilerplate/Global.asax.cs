using System;
using MvcTurbine.Web;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;

namespace DotnetMvcBoilerplate
{
    public class MvcApplication : TurbineApplication
    {
        public MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}