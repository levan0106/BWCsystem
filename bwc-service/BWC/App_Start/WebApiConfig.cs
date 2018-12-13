using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BWC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // WebAPI Delete/Put not working - 405 Method Not Allowed
            //<system.webServer>
            //<validation validateIntegratedModeConfiguration="false"/>
            //<modules runAllManagedModulesForAllRequests="true">
            //    <remove name="WebDAVModule"/> <!-- ADD THIS -->
            //</modules>
            //... rest of settings here
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            config.Filters.Add(new AuthorizeAttribute());

            config.Routes.MapHttpRoute(
                name: "authorization",
                routeTemplate: "api/authorization/{userName}/{password}",
                defaults: new { controller = "Authorization", action = "login", password = RouteParameter.Optional, userName = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/action/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.EnableSystemDiagnosticsTracing();
            //That makes sure you get json on most queries, but you can get xml when you send text/xml.
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
