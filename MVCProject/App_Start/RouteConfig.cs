using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                 name:"HospitalAdminShedules",
                 url:"HospitalAdminSchedule/Shedule/{h_Id}/{d_Id}",
                 new { Controller="HospitalAdminShedules",action="Create"}
                
                );*/
            routes.MapRoute(
               name: "Shedule",
               url: "HospitalAdminSchedule/Create/{id}/{id1}",
               defaults: new { controller = "HospitalAdminShedules", action = "Create", id = UrlParameter.Optional,id1=UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
        }
    }
}
