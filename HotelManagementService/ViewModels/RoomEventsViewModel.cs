using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementService.Models;

namespace HotelManagementService.ViewModels
{
  public class RoomEventsViewModel
  {
    public RoomModels Room { get; set; }
    public IEnumerable<Event> Events { get; set; }
    public Dictionary<Event, List<DateTime>> EventStayTerm { get; set; }

  }
}