using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HotelManagementService.DAL.Context;
using HotelManagementService.Models;
using HotelManagementService.ViewModels;

namespace HotelManagementService.Controllers
{
  public class HomeController : Controller
  {
    private HotelManagementContext _context = new HotelManagementContext();

    [Authorize]
    public PartialViewResult WeekEvent(DateTime? Date)
    {
      var roomEventsList = new List<RoomEventsViewModel>();

      foreach (var e in _context.RoomModels.OrderBy(x => x.RoomName).ToList())
      {
        var roomEvents = _context.Events.Where(x => x.Room.Id == e.Id).Select(x => x).ToList();
        var eventStayTerm = GetEventStayTerm(roomEvents);
        var roomEventsViewModel = new RoomEventsViewModel
        {
          Room = e,
          Events = roomEvents,
          EventStayTerm = eventStayTerm
        };

        roomEventsList.Add(roomEventsViewModel);
      }
      ViewBag.AcctualDate = Date ?? DateTime.Now.Date;
      
      return PartialView(roomEventsList);
    }

    [Authorize]
    public ActionResult Index()
    {
      return View();
    }

    private Dictionary<Event, List<DateTime>> GetEventStayTerm(IEnumerable<Event> eventList)
    {
      return eventList.ToDictionary(e => e, e => SetStayTermDate(e.ArriveDate, e.DepatureDate));
    }

    private List<DateTime> SetStayTermDate(DateTime ArriveDate, DateTime DepatureDate)
    {
      var diffrenceDay = (int)(DepatureDate - ArriveDate).TotalDays;
      diffrenceDay++;
      var stayTermDate = new List<DateTime>();
      for (var i = 0; i < diffrenceDay; i++)
      {
        stayTermDate.Add(ArriveDate.AddDays(i));
      }

      return stayTermDate;
    }

    public ViewResult Test()
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
