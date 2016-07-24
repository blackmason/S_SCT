using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SCT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{mode}/{id}",
                defaults: new { controller = "Almighty", action = "CommandCenter", mode = UrlParameter.Optional , id = UrlParameter.Optional }
            );
        }
    }
}