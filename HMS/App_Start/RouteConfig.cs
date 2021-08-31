using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection oRoutes)
        {
            oRoutes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            oRoutes.MapRoute(
                name: "UAccomodations",
                url: "Accomodations",
                defaults: new { area = "", controller = "Accomodations", action = "Index" },
                namespaces: new[] { "HMS.Controllers" }
            );

            oRoutes.MapRoute(
                name: "PackageDetails",
                url: "Details/{iAcom}",
                defaults: new { area = "", controller = "Accomodations", action = "Details" },
                namespaces: new[] { "HMS.Controllers" }
            );

            oRoutes.MapRoute(
                name: "BookingDetails",
                url: "Booking/{iPack}",
                defaults: new { area = "", controller = "Accomodations", action = "Booking" },
                namespaces: new[] { "HMS.Controllers" }
            );

            oRoutes.MapRoute(
                name: "CheckAvailability",
                url: "Availability",
                defaults: new { area = "", controller = "Accomodations", action = "Availability" },
                namespaces: new[] { "HMS.Controllers" }
            );

            oRoutes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
