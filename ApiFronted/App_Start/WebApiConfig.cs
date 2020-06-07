using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFrontend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
#if !DEBUG
            config.Routes.IgnoreRoute("help", "help"); // Make help page, which is now unavailable on release builds, throw a 404 instead of a 500.
#endif
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("https://localhost:4200", "*", "*");
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
