using System;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;
using AutoMoq;
using DotnetMvcBoilerplate.ViewModels.Login;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Service;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class LoginControllerTests
    {
        private AutoMoqer _autoMoqer;

        [SetUp]
        public void SetUp()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that Index returns the view.
        /// </summary>
        [Test]
        public void Index_ReturnsViewResult()
        {
            Assert.That(_autoMoqer.Resolve<LoginController>().Index(), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that a POST request to Index with valid credentials returns
        /// the client to the home page.
        /// </summary>
        [Test]
        public void Index_PostWithValidCredentials_RedirectsClientToRoot()
        {
            var validUsername = "Username";
            var validPassword = "Password";

            SetupAuth(validUsername, validPassword, true);

            Assert.That((Login(validUsername, validPassword, false) as RedirectResult).Url, Is.EqualTo("/"));
        }

        /// <summary>
        /// Tests that a POST request to Index with invalid credentials returns
        /// the client to the form.
        /// </summary>
        [Test]
        public void Index_PostWithInvalidCredentials_ReturnsClientToForm()
        {
            var invalidUsername = "Username";
            var invalidPassword = "Password";

            SetupAuth(invalidUsername, invalidPassword, false);

            Assert.That(Login(invalidUsername, invalidPassword, false), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that a POST request to Index with invalid credentials sets
        /// user feedback on the ViewData object.
        /// </summary>
        [Test]
        public void Index_PostWithInvalidCredentials_SetsFeedbackOnViewData()
        {
            var invalidUsername = "Username";
            var invalidPassword = "Password";

            SetupAuth(invalidUsername, invalidPassword, false);

            var result = Login(invalidUsername, invalidPassword, false);

            Assert.That((result as ViewResult).ViewData["Feedback"], !Is.Null);
        }

        /// <summary>
        /// Calls Index on the LoginController with mock user entered data.
        /// </summary>
        /// <param name="username">Username entered by the user.</param>
        /// <param name="password">Password entered by the user.</param>
        /// <param name="remember">Remember me entered by the user.</param>
        /// <returns>Result from LoginController.Index.</returns>
        public ActionResult Login(string username, string password, bool remember)
        {
            return _autoMoqer.Resolve<LoginController>().Index(CreateLoginViewModel(username, password, remember));
        }

        /// <summary>
        /// Creates a mock of LoginViewModel.
        /// </summary>
        /// <param name="username">Value returned by Username.</param>
        /// <param name="password">Value returned by Password.</param>
        /// <param name="remember">Value returned by RememberMe.</param>
        /// <returns>Mock of LoginViewModel.</returns>
        public LoginViewModel CreateLoginViewModel(string username, string password, bool remember)
        {
            var mock = _autoMoqer.GetMock<LoginViewModel>();
            mock.Setup(x => x.Username).Returns(username);
            mock.Setup(x => x.Password).Returns(password);
            mock.Setup(x => x.RememberMe).Returns(remember);
            return mock.Object;
        }

        /// <summary>
        /// Sets up the service call for authenticating the credentials.
        /// </summary>
        /// <param name="username">Expected username.</param>
        /// <param name="password">Expected password.</param>
        /// <param name="returned">Result of the authentication.</param>
        public void SetupAuth(string username, string password, bool success)
        {
            User userReturned = null;

            if (success)
            {
                var mockUserReturned = _autoMoqer.GetMock<User>();
                mockUserReturned.Setup(x => x.Username).Returns(username);
                userReturned = mockUserReturned.Object;
            }

            var mock = _autoMoqer.GetMock<IUserService>();
            mock.Setup(x => x.ByUsernameAndPassword(username, password)).Returns(userReturned);
        }
    }
}
