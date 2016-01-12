using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementService.Models;

namespace HotelManagementService.ViewModels
{
  public class RoomsEventsViewModel
  {
    public ICollection<RoomModels> Rooms { get; set; }
    public ICollection<Event> Events { get; set; }  
  }
}