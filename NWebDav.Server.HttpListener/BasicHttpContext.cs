using System;
using System.Net;
using System.Security;
using System.Security.Principal;

using NWebDav.Server.Http;

namespace NWebDav.Server.HttpListener
{
    /// <inheritdoc />
    public class HttpBasicContext : HttpBaseContext
    {
        /// <inheritdoc />
        public HttpBasicContext(HttpListenerContext httpListenerContext, Func<HttpListenerBasicIdentity, IPrincipal> getPrincipal) : base(httpListenerContext.Request, httpListenerContext.Response)
        {
            // Obtain the basic identity
            var basicIdentity = httpListenerContext.User?.Identity as HttpListenerBasicIdentity;

            // Resolve to a principal
            var principal = getPrincipal(basicIdentity);

            // Create the session
            Session = new HttpSession(principal);
        }

        /// <inheritdoc />
        public HttpBasicContext(HttpListenerContext httpListenerContext, Func<HttpListenerBasicIdentity, bool> checkIdentity) : base(httpListenerContext.Request, httpListenerContext.Response)
        {
            // Obtain the basic identity
            var basicIdentity = httpListenerContext.User?.Identity as HttpListenerBasicIdentity;
            if (!checkIdentity(basicIdentity))
                throw new SecurityException("Basic authorization failed.");

            // Create the session
            Session = new HttpSession(httpListenerContext.User);
        }

        public override IHttpSession Session { get; }
    }
}
