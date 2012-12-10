using System;
using System.Linq;
using NUnit.Framework;
using System.Web;
using AutoMoq;
using DotnetMvcBoilerplate.Core.Security;
using DotnetMvcBoilerplate.Tests.Unit.Utils;
using System.Web.Security;
using System.Configuration;
using DotnetMvcBoilerplate.Core.Provider;
using DotnetMvcBoilerplate.Models;
using Moq;
using MongoDB.Bson;
using System.Security.Principal;

namespace DotnetMvcBoilerplate.Tests.Unit.Core.Security
{
    public class SessionAuthenticationTests
    {
        private const string CookieName = "CookieName";

        private AutoMoqer _autoMoqer;

        /// <summary>
        /// Amount of minutes that a cookie should be active for.
        /// </summary>
        private static int InactivateSessionTimeout
        {
            get { return int.Parse(ConfigurationManager.AppSettings["InactivateSessionTimeout"]); }
        }

        /// <summary>
        /// Amount of days that the cookie should be remembered for.
        /// </summary>
        private static int RememberMeTimeout
        {
            get { return int.Parse(ConfigurationManager.AppSettings["RememberMeTimeout"]); }
        }

        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that End method nullifies the Username that 
        /// is stored on the Session.
        /// </summary>
        [Test]
        public void End_ShouldNullifyLoggedInAsOnSession()
        {
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().End();

            _autoMoqer.GetMock<HttpSessionStateBase>().VerifySet(x => x["LoggedInAs"] = null, Times.Once());
        }

        /// <summary>
        /// Tests that the Username of the User is set on the
        /// Session when calling SetSessionData.
        /// </summary>
        [Test]
        public void SetSessionData_SetsUsernameOnSession()
        {
            var user = Fakes.Users()[0];
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().SetSessionData(user);

            _autoMoqer.GetMock<HttpSessionStateBase>().VerifySet(x => x["LoggedInAs"] = user.Username, Times.Once());
        }

        /// <summary>
        /// Tests that when calling Start, SetSessionData
        /// should also occur.
        /// </summary>
        [Test]
        public void Start_ShouldSetSessionData()
        {
            var user = Fakes.Users()[0];
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(user, false); 

            _autoMoqer.GetMock<HttpSessionStateBase>().VerifySet(x => x["LoggedInAs"] = user.Username, Times.Once());
        }

        /// <summary>
        /// Tests that the Start method adds a HttpCookie to the
        /// Cookies collection in the response of the HttpContext.
        /// </summary>
        [Test]
        public void Start_AddsCookieToHttpResponse()
        {
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(Fakes.Users()[0], false); 

            Assert.That(_autoMoqer.GetMock<HttpResponseBase>().Object.Cookies.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that Start adds a cookie with an encrypted ticket that
        /// can be associated to the logged in user.
        /// </summary>
        [Test]
        public void Start_AddsEncryptedTicketThatIdentifiesTheUserToCookie()
        {
            var user = Fakes.Users()[0];
            var expectedName = user.Id.ToString();
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(user, false);

            Assert.That(GetDecryptedTicket().Name, Is.EqualTo(expectedName));
        }

        /// <summary>
        /// Tests that Start with remember set as false adds a cookie with an encrypted ticket
        /// that contains a small expiration date.
        /// </summary>
        [Test]
        public void Start_NotSelectedRemember_AddsEncryptedTicketWithShortExpirationDateToCookie()
        {
            var user = Fakes.Users()[0];
            var expectedExpirationDate = DateTime.Now.AddMinutes(InactivateSessionTimeout);
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(user, false);

            Assert.That(GetDecryptedTicket().Expiration.ToString(), Is.EqualTo(expectedExpirationDate.ToString()));
        }

        /// <summary>
        /// Tests that Start with rememeber set as true adds a cookie with an encrypted ticket
        /// that contains a long expiration date.
        /// </summary>
        [Test]
        public void Start_RememberMe_AddsEncyrptedTicketWithLongExpirationDateToCookie()
        {
            var user = Fakes.Users()[0];
            var expectedExpirationDate = DateTime.Now.AddDays(RememberMeTimeout);
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(user, true);

            Assert.That(GetDecryptedTicket().Expiration.ToShortDateString(), Is.EqualTo(expectedExpirationDate.ToShortDateString()));
        }

        /// <summary>
        /// Tests that Start with remember set as true sets the long expiration 
        /// date on the cookie as well as the encrypted ticket.
        /// </summary>
        [Test]
        public void Start_RememberMe_SetsLongExpirationDateOnCookie()
        {
            var user = Fakes.Users()[0];
            var expectedExpirationDate = DateTime.Now.AddDays(RememberMeTimeout);
            SetupMockParameters();

            _autoMoqer.Resolve<SessionAuthentication>().Start(user, true);

            var cookie = _autoMoqer.GetMock<HttpResponseBase>().Object.Cookies[0];
            Assert.That(cookie.Expires.ToShortDateString(), Is.EqualTo(expectedExpirationDate.ToShortDateString()));
        }

        /// <summary>
        /// Gets the FormsAuthenticationTicket that is encrypted and stored
        /// inside the Cookie.
        /// </summary>
        /// <returns>Decrypted FormsAuthenticationTicket holding details
        /// about the users authenticated session.</returns>
        private FormsAuthenticationTicket GetDecryptedTicket()
        {
            var cookie = _autoMoqer.GetMock<HttpResponseBase>().Object.Cookies[0];
            return FormsAuthentication.Decrypt(cookie.Value);
        }

        /// <summary>
        /// Sets up the mock parameters for SessionAuthentication.
        /// </summary>
        private void SetupMockParameters()
        {
            SetupMockParameters(null);
        }

        /// <summary>
        /// Sets up the mock parameters for SessionAuthentication.
        /// </summary>
        /// <param name="ticket">Ticket to be returned by the FormsIdentity
        /// on the user property.</param>
        /// <returns>FormsIdentity set on User.Identity on Context.</returns>
        private void SetupMockParameters(FormsIdentity identity)
        {
            var response = _autoMoqer.GetMock<HttpResponseBase>();
            response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());

            var session = _autoMoqer.GetMock<HttpSessionStateBase>();

            var context = _autoMoqer.GetMock<HttpContextBase>();

            _autoMoqer.GetMock<IHttpContextProvider>().Setup(x => x.Context).Returns(context.Object);
            _autoMoqer.GetMock<IHttpContextProvider>().Setup(x => x.Response).Returns(response.Object);
            _autoMoqer.GetMock<IHttpContextProvider>().Setup(x => x.Session).Returns(session.Object);

            if (identity != null)
            {
                var principal = _autoMoqer.GetMock<IPrincipal>();
                principal.Setup(x => x.Identity).Returns(identity);
                context.Setup(x => x.User).Returns(principal.Object);
            }
        }
    }
}
