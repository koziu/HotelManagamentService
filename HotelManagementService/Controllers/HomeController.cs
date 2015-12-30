using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;       

namespace HotelManagementService.Controllers
{
  public class HomeController : Controller
  {      
    [Authorize]
    public ActionResult Index()
    {
      return View();
    }

    //public ActionResult About()
    //{
    //  return View();
    //}

    //public ActionResult Contact()
    //{
    //  return View();
    //}
  }
}
