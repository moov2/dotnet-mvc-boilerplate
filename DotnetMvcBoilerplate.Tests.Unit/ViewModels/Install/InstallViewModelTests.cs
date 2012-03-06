using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.ViewModels.Install;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Tests that validation is done to ensure that the Password
        /// has been entered.
        /// </summary>
        [Test]
        public void Validation_PasswordIsRequired()
        {
            var propertyInfo = typeof(InstallViewModel).GetProperty("Password");

            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)
                                          .Cast<RequiredAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute, !Is.Null);
        }

        /// <summary>
        /// Tests that validation is done to ensure that the Username
        /// has been entered.
        /// </summary>
        [Test]
        public void Validation_UsernameIsRequired()
        {
            var propertyInfo = typeof(InstallViewModel).GetProperty("Username");

            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)
                                          .Cast<RequiredAttribute>()
                                          .FirstOrDefault();

            Assert.That(attribute, !Is.Null);
        }
    }
}
