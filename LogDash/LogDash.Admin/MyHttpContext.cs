using Microsoft.AspNetCore.Http;
using System;

namespace Framework.Platform.Core
{
    public class MyHttpContext
    {
        public static IServiceProvider ServiceProvider;

        static MyHttpContext()
        { }


        public static HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));

                HttpContext context = ((Microsoft.AspNetCore.Http.IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}
