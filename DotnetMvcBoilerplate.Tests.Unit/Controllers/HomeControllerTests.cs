using System;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        /// <summary>
        /// Tests that Index returns the view.
        /// </summary>
        [Test]
        public void Index_ReturnsViewResult()
        {
            Assert.That(new HomeController().Index(), Is.InstanceOf<ViewResult>());
        }
    }
}
