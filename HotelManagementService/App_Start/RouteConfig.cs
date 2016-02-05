using System.Web.Mvc;
using System.Web.Routing;

namespace HotelManagementService
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute("Statistic", "{controller}/{action}",
        new { controller = "Statistic", action = "Index"}
        );
      routes.MapMvcAttributeRoutes();

      routes.MapRoute("Default", "{controller}/{action}/{id}",
        new {controller = "Home", action = "Index", id = UrlParameter.Optional}
        );
    }
  }
}