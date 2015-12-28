using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementService.Enums
{
  public enum ReservationStates
  {
    Unconfirmed,
    Confirmed,
    Canceled,
    Arrival,
    Stay,
    Depature,
    NoShow,
    Closed
  }
}