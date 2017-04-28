﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "SysUser", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            );
            //routes.MapRoute(
            //    name: "Default",
           //   url: "{controller}/{action}/{id}",
           //     defaults: new { controller = "SysUser", action = "LoginMobile", id = UrlParameter.Optional }
           // );
        }
    }
}