using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;
using DotnetMvcBoilerplate.Core.Security.Attributes;

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
            Assert.That(new AdminController().Index(), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that only Admin users can see the Index
        /// page.
        /// </summary>
        [Test]
        public void Index_OnlyAllowsAdminUsers()
        {
            var propertyInfo = typeof(AdminController).GetMethod("Index");

            var attribute = propertyInfo.GetCustomAttributes(typeof(AdminOnlyAttribute), false)
                                          .Cast<AdminOnlyAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute, !Is.Null);
        }
    }
}
