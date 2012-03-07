using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.ViewModels.Install;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Tests.Unit.ViewModels.Install
{
    public class InstallViewModelTests
    {
        /// <summary>
        /// Tests that the Username is set on the User returned from ToUser.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserWithUsername()
        {
            const string expectedUsername = "My Username";
            Assert.That(new InstallViewModel { Username = expectedUsername, Password = "Password" }.ToUser().Username, Is.EqualTo(expectedUsername));
        }

        /// <summary>
        /// Tests that the User returned from ToUser is an admin.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserAsAnAdmin()
        {
            Assert.That(new InstallViewModel { Password = "Password" }.ToUser().IsAdmin, Is.True);
        }

        /// <summary>
        /// Tests that the User returned from ToUser has an encrypted
        /// password.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserWithEncryptedPassword()
        {
            const string usersPassword = "password";
            var actualPassword = new InstallViewModel { Password = usersPassword }.ToUser().Password;

            Assert.That(new InstallViewModel { Password = usersPassword }.ToUser().Password, !Is.Null);
        }

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
