﻿using System.Web;
using NWebDav.Server.Http;

namespace NWebDav.Server.AspNet
{
    public partial class AspNetContext : IHttpContext
    {
        private readonly AspNetRequest _request;
        private readonly AspNetSession _session;
        private readonly AspNetResponse _response;

        public AspNetContext(HttpContext httpContext)
        {
            // Assign properties
            _request = new AspNetRequest(httpContext.Request);
            _session = new AspNetSession(httpContext);
            _response = new AspNetResponse(httpContext.Response);
        }

        public IHttpRequest Request => _request;
        public IHttpResponse Response => _response;
        public IHttpSession Session => _session;

        public void Close()
        {
            // NOP
        }
    }
}