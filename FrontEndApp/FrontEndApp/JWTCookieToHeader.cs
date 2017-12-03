using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp
{
    public class JWTCookieToHeader
    {
        private readonly RequestDelegate _next;

        public JWTCookieToHeader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["access_token"];
            if (token != null)
            {
                context.Request.Headers.Append("Authorization", "Bearer " + token);
            }

            await _next.Invoke(context);
        }
    }
}
