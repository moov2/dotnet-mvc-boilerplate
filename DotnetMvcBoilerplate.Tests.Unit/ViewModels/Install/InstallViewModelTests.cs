using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.ViewModels.Install;
using DataAnnotationsExtensions;

namespace DotnetMvcBoilerplate.Tests.Unit.ViewModels.Install
{
    public class InstallViewModelTests
    {
        /// <summary>
        /// Tests that validation is done to ensure that the ConfirmPassword
        /// is equal to Password.
        /// </summary>
        [Test]
        public void Validation_ConfirmPasswordIsEqualToPassword()
        {
            var propertyInfo = typeof(InstallViewModel).GetProperty("ConfirmPassword");

            var attribute = propertyInfo.GetCustomAttributes(typeof(EqualToAttribute), false)
                                          .Cast<EqualToAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute.OtherProperty, Is.EqualTo("Password"));
        }
    }
}
