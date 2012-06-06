using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.ViewModels.Install;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using DotnetMvcBoilerplate.Core.Security;
using DotnetMvcBoilerplate.Models;
using AutoMoq;

namespace DotnetMvcBoilerplate.Tests.Unit.ViewModels.Install
{
    public class InstallViewModelTests
    {
        private AutoMoqer _autoMoqer;

        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that the Username is set on the User returned from ToUser.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserWithUsername()
        {
            const string expectedUsername = "My Username";
            const string userEnteredPassword = "Password";

            SetupPasswordEncryption(userEnteredPassword);

            Assert.That(ToUserWithUsernameAndPassword(expectedUsername, userEnteredPassword).Username, Is.EqualTo(expectedUsername));
        }

        /// <summary>
        /// Tests that the User returned from ToUser is an admin.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserAsAnAdmin()
        {
            const string userEnteredPassword = "Password";
            SetupPasswordEncryption(userEnteredPassword);

            Assert.That(ToUserWithUsernameAndPassword("Username", userEnteredPassword).IsAdmin, Is.True);
        }

        /// <summary>
        /// Tests that the User returned from ToUser has an encrypted
        /// password.
        /// </summary>
        [Test]
        public void ToUser_ReturnsUserWithEncryptedPassword()
        {
            const string userEnteredPassword = "Password";
            SetupPasswordEncryption(userEnteredPassword);

            var user = ToUserWithUsernameAndPassword("Username", userEnteredPassword);
            Assert.That(user.PasswordKey == _autoMoqer.GetMock<Password>().Object.Key && user.PasswordSalt == _autoMoqer.GetMock<Password>().Object.Salt, Is.True);
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

        /// <summary>
        /// Sets up the IEncryption to ensure that a password is entered.
        /// </summary>
        /// <param name="password">The password to be encrypted.</param>
        private void SetupPasswordEncryption(string password)
        {
            var mockExpectedPassword = _autoMoqer.GetMock<Password>();
            mockExpectedPassword.Setup(x => x.Key).Returns(new byte[8]);
            mockExpectedPassword.Setup(x => x.Salt).Returns(new byte[8]);

            _autoMoqer.GetMock<IEncryption>().Setup(x => x.Encrypt(password)).Returns(mockExpectedPassword.Object);
        }

        /// <summary>
        /// Creates an instance of InstallViewModel with the username and password provide,
        /// then returns the value returned from ToUser.
        /// </summary>
        /// <param name="username">Value of the Username property on InstallViewModel.</param>
        /// <param name="password">Value of the password property on InstallViewModel.</param>
        /// <returns>Value returned from ToUser.</returns>
        private User ToUserWithUsernameAndPassword(string username, string password)
        {
            return new InstallViewModel { Username = username, Password = password }.ToUser(_autoMoqer.GetMock<IEncryption>().Object);
        }
    }
}
