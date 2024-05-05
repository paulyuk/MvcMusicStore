using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MvcMusicStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "albums",
                routeTemplate: "api/albums/"
            );
        }
    }
}
