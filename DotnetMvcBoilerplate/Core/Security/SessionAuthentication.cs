using System;
using System.Linq;
using System.Web;
using DotnetMvcBoilerplate.Models;
using System.Web.Security;
using System.Configuration;
using DotnetMvcBoilerplate.Core.Provider;

namespace DotnetMvcBoilerplate.Core.Security
{
    public class SessionAuthentication : ISessionAuthentication
    {
        private IHttpContextProvider _httpContextProvider;

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

        public SessionAuthentication(IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider;
        }

        /// <summary>
        /// Creates an encrypted FormsAuthenticationTicket tied to the username and setting the roles onto
        /// the UserData property.
        /// </summary>
        /// <param name="username">Name of the ticket.</param>
        /// <param name="roles">Comma seperated list of the roles that the user belongs too.</param>
        /// <returns>Encrypted hash of a FormsAuthenticationTicket</returns>
        private static string CreateEncryptedTicket(string username, string roles, DateTime expireAt, bool isPersistent)
        {
            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, expireAt, isPersistent, roles, FormsAuthentication.FormsCookiePath);
            return FormsAuthentication.Encrypt(ticket);
        }

        /// <summary>
        /// Gets the date that the ticket should expire based if the user wants to be remembered
        /// when they next visit the site.
        /// </summary>
        /// <param name="remember">Flag indicating whether the user should be rememebered.</param>
        /// <returns>Expiry date for the ticket.</returns>
        private static DateTime GetExpiryDate(bool remember)
        {
            return (remember) ? DateTime.Now.AddDays(RememberMeTimeout) : DateTime.Now.AddMinutes(InactivateSessionTimeout);
        }

        /// <summary>
        /// Authenticates the session, which changes the clients
        /// state to logged in.
        /// </summary>
        /// <param name="user">User that has signed in.</param>
        /// <param name="remember">Whether the user should be
        /// remembered next time they visit.</param>
        public void Start(User user, bool remember)
        {
            var roles = string.Join(",", user.Roles.ToArray());
            var expireDate = GetExpiryDate(remember);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, CreateEncryptedTicket(user.Id.ToString(), roles, expireDate, remember));

            if (remember)
                cookie.Expires = expireDate;

            _httpContextProvider.Response.Cookies.Add(cookie);
        }
    }

    public interface ISessionAuthentication
    {
        void Start(User user, bool remember);
    }
}