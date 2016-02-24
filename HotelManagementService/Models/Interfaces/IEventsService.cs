using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementService.Models.Interfaces
{
  public interface IEventsService
  {
    List<Event> GetEvents();
    Event GetEvent(Guid? eventId);
    void AddEvent(Event @event);
    void DeleteEvent(Event @event);
    void EditEvent(Event @event);
  }
}
