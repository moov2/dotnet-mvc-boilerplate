using System;
using System.Web;

namespace DotnetMvcBoilerplate.Core.Provider
{
    public class HttpContextProvider : IHttpContextProvider
    {
        /// <summary>
        /// Returns the current HttpContext.
        /// </summary>
        public HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        /// <summary>
        /// Returns the HttpResponse that is stored
        /// within the current HttpContext.
        /// </summary>
        public HttpResponseBase Response
        {
            get { return Context.Response; }
        }

        /// <summary>
        /// Returns the HttpSessionState that is stored
        /// within the current HttpContext.
        /// </summary>
        public HttpSessionStateBase Session
        {
            get { return Context.Session; }
        }
    }

    public interface IHttpContextProvider
    {
        HttpContextBase Context { get; }
        HttpResponseBase Response { get; }
        HttpSessionStateBase Session { get; }

    }
}