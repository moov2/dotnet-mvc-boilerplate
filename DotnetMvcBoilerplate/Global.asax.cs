using System;
using MvcTurbine.Web;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using DotnetMvcBoilerplate.Core.IO;

namespace DotnetMvcBoilerplate
{
    public class MvcApplication : TurbineApplication
    {
        public MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }

        public override void Startup()
        {
            WindowsService.StartIfNotRunning(WindowsService.MongoDB);
        }
    }
}