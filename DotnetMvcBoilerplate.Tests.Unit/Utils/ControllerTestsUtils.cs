using System;
using System.Web.Mvc;
using Moq;
namespace DotnetMvcBoilerplate.Tests.Unit.Utils
{
    public class ControllerTestsUtils
    {
        /// <summary>
        /// Sets the ModelState.IsValid to return false.
        /// </summary>
        /// <param name="controller">Controller that the ModelState is set on.</param>
        public static void SetModelStateAsInvalid(Controller controller)
        {
            controller.ModelState.AddModelError("Fake Error", "This is a fake error message.");
        }
    }
}
