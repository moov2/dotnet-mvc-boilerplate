using System;
using System.Web.Mvc;
using Moq;
using AutoMoq;
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;
namespace DotnetMvcBoilerplate.Tests.Unit.Utils
{
    public class ControllerTestsUtils
    {
        /// <summary>
        /// Sets the ModelState.IsValid to return false.
        /// </summary>
        /// <param name="controller">Controller that the ModelState is set on.</param>
        public static void SetModelStateAsInvalid(Controller controller)
        {
            controller.ModelState.AddModelError("Fake Error", "This is a fake error message.");
        }

        /// <summary>
        /// Sets a mock HttpRequestBase onto the controller provided.
        /// </summary>
        /// <param name="controller"></param>
        public static void SetMockContext(AutoMoqer autoMoqer, Controller controller)
        {
            var context = autoMoqer.GetMock<HttpContextBase>();
            var request = autoMoqer.GetMock<HttpRequestBase>();
            request.Setup(x => x.QueryString).Returns(new NameValueCollection());

            context.Setup(x => x.Request).Returns(request.Object);

            var requestContext = new RequestContext(context.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(requestContext, controller);
        }
    }
}
