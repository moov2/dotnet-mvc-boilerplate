using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class ProfileControllerTests
    {
        /// <summary>
        /// Tests that unauthorized users are not able to
        /// access the 
        /// </summary>
        [Test]
        public void Index_UnauthorisedUsersCantAccess()
        {
            var propertyInfo = typeof(ProfileController).GetMethod("Index");

            var attribute = propertyInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false)
                                          .Cast<AuthorizeAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute, !Is.Null);
        }
    }
}
