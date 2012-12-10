using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class AdminControllerTests
    {
        /// <summary>
        /// Tests that Index returns the view.
        /// </summary>
        [Test]
        public void Index_ReturnsView()
        {
            Assert.That(new AuthorisedController().Index(), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that only authorised users can see the Index page.
        /// </summary>
        [Test]
        public void Index_OnlyAllowsAuthorisedUsers()
        {
            var propertyInfo = typeof(AuthorisedController).GetMethod("Index");

            var attribute = propertyInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false)
                                          .Cast<AuthorizeAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute, !Is.Null);
        }
    }
}
