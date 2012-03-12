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
            get { return new HttpResponseWrapper(HttpContext.Current.Response); }
        }
    }

    public interface IHttpContextProvider
    {
        HttpContext Context { get; }
        HttpResponseBase Response { get; }
    }
}