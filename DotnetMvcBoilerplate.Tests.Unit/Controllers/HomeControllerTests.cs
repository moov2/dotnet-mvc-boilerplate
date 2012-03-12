using System;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;
using AutoMoq;
using DotnetMvcBoilerplate.Core.Security;
using Moq;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        private AutoMoqer _autoMoqer;

        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that Index returns the view.
        /// </summary>
        [Test]
        public void Index_ReturnsViewResult()
        {
            Assert.That(_autoMoqer.Resolve<HomeController>().Index(), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that Logout redirects the client to the
        /// login form.
        /// </summary>
        [Test]
        public void Logout_RedirectsClientToLoginForm()
        {
            Assert.That((_autoMoqer.Resolve<HomeController>().Logout() as RedirectResult).Url, Is.EqualTo("/Login"));
        }

        /// <summary>
        /// Tests that Logout calls End on the SessionAuthentication.
        /// </summary>
        [Test]
        public void Logout_CallsEndOnSessionAuthentication()
        {
            _autoMoqer.Resolve<HomeController>().Logout();

            _autoMoqer.GetMock<ISessionAuthentication>().Verify(x => x.End(), Times.Once());
        }
    }
}
