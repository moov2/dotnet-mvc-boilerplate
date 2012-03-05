using System;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class LoginControllerTests
    {
        /// <summary>
        /// Tests that Index returns the view.
        /// </summary>
        [Test]
        public void Index_ReturnsViewResult()
        {
            Assert.That(new LoginController().Index(), Is.InstanceOf<ViewResult>());
        }
    }
}
