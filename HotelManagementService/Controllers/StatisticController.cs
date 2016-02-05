using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using HotelManagementService.DAL.Context;
using HotelManagementService.ViewModels;

namespace HotelManagementService.Controllers
{
  [RoutePrefix("Statistic")]
  public class StatisticController : Controller
  {
    private ClientCountPerMonth[] _clientCountPerMonths = new ClientCountPerMonth[12];
    private HotelManagementContext _db = new HotelManagementContext();

    // GET: Statistic

    [HttpGet]
    //[Route("Index/{year}")]
    public ActionResult Index(int year = 2016)
    {
      SetClientsPerMonth(year);
      return View();
    }

    private void SetClientsPerMonth(int year)
    {
      for (var i = 0; i < 12; i++)
      {
        var firstDayMonthPar = new SqlParameter("firstDayMonth", new DateTime(year, i + 1, 1));
        var lastMonthDayPar = new SqlParameter("lastMonthDay", new DateTime(year, i + 1, DateTime.DaysInMonth(year, i + 1)));
        _clientCountPerMonths[i] = new ClientCountPerMonth
        {
          Month = i,
          ClientCount = _db.Database.SqlQuery<int?>(@"exec CountClientPerMonth  @firstDayMonth, @lastMonthDay", firstDayMonthPar, lastMonthDayPar).First()
        };
      }
    }
  }

  
//create procedure CountClientPerMonth 
//@firstDayMonth datetime2(7),
//@lastMonthDay datetime2(7)
//as
//begin
//SELECT sum(r.BEdDBCount + r.BedSBCOunt)from dbo.Events e join dbo.Room r on e.Room_Id = r.Id where e.ArriveDate between convert(datetime2,@firstDayMonth) and convert(datetime2,@lastMonthDay) or e.DepatureDate between  convert(datetime2,@firstDayMonth) and convert(datetime2,@lastMonthDay)
//end
}