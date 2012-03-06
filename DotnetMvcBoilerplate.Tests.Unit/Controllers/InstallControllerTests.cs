using System;
using NUnit.Framework;
using DotnetMvcBoilerplate.Controllers;
using System.Web.Mvc;
using AutoMoq;
using DotnetMvcBoilerplate.ViewModels.Install;
using DotnetMvcBoilerplate.Tests.Unit.Utils;

namespace DotnetMvcBoilerplate.Tests.Unit.Controllers
{
    public class InstallControllerTests
    {
        private AutoMoqer _autoMoqer;

        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that a GET request to Index will return the install form.
        /// </summary>
        [Test]
        public void Index_HttpGet_ReturnsView()
        {
            Assert.That(_autoMoqer.Resolve<InstallController>().Index(), Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        /// Tests that a POST request with an invalid Model will return
        /// the install form with the model passed to Index.
        /// </summary>
        [Test]
        public void Index_HttpPostWithInvalidModel_ReturnsViewWithModel()
        {
            var expectedModel = _autoMoqer.Create<InstallViewModel>();
            var installController = _autoMoqer.Resolve<InstallController>();
            ControllerTestsUtils.SetModelStateAsInvalid(installController);

            Assert.That((installController.Index(expectedModel) as ViewResult).Model, Is.EqualTo(expectedModel));
        }

        /// <summary>
        /// Tests that a POST request with a valid Model will redirect
        /// the user to the home page.
        /// </summary>
        [Test]
        public void Index_HttpPostWithValidModel_RedirectsToRoot()
        {
            Assert.That((_autoMoqer.Resolve<InstallController>().Index(_autoMoqer.Create<InstallViewModel>()) as RedirectResult).Url, Is.EqualTo("/"));
        }
    }
}
