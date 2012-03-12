using System;
using System.Web;

namespace DotnetMvcBoilerplate.Core.Provider
{
    public class HttpContextProvider : IHttpContextProvider
    {
        /// <summary>
        /// Returns the current HttpContext.
        /// </summary>
        public HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        /// <summary>
        /// Returns the HttpResponse that is stored
        /// within the current HttpContext.
        /// </summary>
        public HttpResponseBase Response
        {
            get { return new HttpResponseWrapper(Context.Response); }
        }

        /// <summary>
        /// Returns the HttpSessionState that is stored
        /// within the current HttpContext.
        /// </summary>
        public HttpSessionStateBase Session
        {
            get { return new HttpSessionStateWrapper(Context.Session); }
        }
    }

    public interface IHttpContextProvider
    {
        HttpContext Context { get; }
        HttpResponseBase Response { get; }
        HttpSessionStateBase Session { get; }

    }
}