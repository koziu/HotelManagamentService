using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HotelManagementService.Models.Interfaces;

namespace HotelManagementService.Models.Service
{
  public class EventsService : Service, IEventsService
  {
    private readonly IDataModelEF _context;

    EventsService(IDataModelEF context) : base(context)
    {
      _context = context;
    }

    public List<Event> GetEvents()
    {
      using (var db = _context.CreateNew())
      {
        return db.Events.ToList();
      }
    }

    public Event GetEvent(Guid? eventId)
    {
      using (var db = _context.CreateNew())
      {
        return db.Events.Find(eventId);
      }
    }

    public void AddEvent(Event @event)
    {
      using (var db = _context.CreateNew())
      {
        db.Events.Add(@event);
        db.SaveChanges();
      }
    }

    public void DeleteEvent(Event @event)
    {
      using (var db = _context.CreateNew())
      {
        db.Events.Attach(@event);
        db.Events.Remove(@event);
        db.SaveChanges();
      }
    }

    public void EditEvent(Event @event)
    {
      using (var db = _context.CreateNew())
      {
        db.Entry(@event).State = EntityState.Modified;
        db.SaveChanges();
      }
    }
  }
}