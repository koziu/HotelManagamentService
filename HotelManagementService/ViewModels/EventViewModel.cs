using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementService.Enums;

namespace HotelManagementService.ViewModels
{
  public class EventViewModel
  {
    public double Price { get; set; }
    public ReservationStates ReservationState { get; set; }
    public DateTime ArriveDate { get; set; }
    public DateTime DepatureDate { get; set; }
  }
}