using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectSafehouse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PartialAddUser",
                url: "User/Partial_AddUserTo/{groupingType}/{groupingId}",
                defaults: new { controller = "User", action = "Partial_AddUserTo", groupingType = UrlParameter.Optional, groupingId = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "Company",
                url: "Company/{action}/{id}",
                defaults: new { controller = "Company", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Project",
                url: "Project/LoadCompanyProjects/{id}",
                defaults: new { controller = "Project", action = "LoadCompanyProjects", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProjectOverview",
                url: "Project/{projectId}/Overview",
                defaults: new { controller = "Project", action = "Overview", projectId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LogOut",
                url: "LogOut/{id}",
                defaults: new { controller = "Login", action = "LogOut", id = UrlParameter.Optional }
            );

            //if there is no controller name, assume we are on the login page.
            //routes.MapRoute(
            //    name: "LoginDefault",
            //    url: "{action}/{id}",
            //    defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}