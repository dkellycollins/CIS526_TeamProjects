using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Ajax",
                url: "{controller}/JS/{action}",
                defaults: new { controller = "Scoreboard", action = "Index" }
            );*/

            routes.MapRoute(
                name: "Scoreboard",
                url: "Scoreboard/{pointType}",
                defaults: new { controller = "Scoreboard", action = "Index", pointType = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Scoreboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}