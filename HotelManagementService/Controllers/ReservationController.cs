﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HotelManagementService.DAL.Context;
using HotelManagementService.Enums;
using HotelManagementService.Models;

namespace HotelManagementService.Controllers
{
  [Authorize]
  public class ReservationController : Controller
  {
    private readonly HotelManagementContext db = new HotelManagementContext();

    // GET: ReservationModels
    public async Task<ActionResult> Index()
    {
      IQueryable<Event> eventsModel = db.Events.Select(x => x);
      return View(await eventsModel.ToListAsync());
      
    }

    // GET: ReservationModels/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var @event = new ClientsReservationModels();
      @event.Event = await db.Events.FindAsync(id);
      

      if (@event == null)
      {
        return HttpNotFound();
      }
      return View(@event);
    }

    // GET: ReservationModels/Create
    public ActionResult Create()
    {
      ViewBag.Id = new SelectList(db.Events, "Id", "Id");
      return View(GetClientsReservationModels());
    }

    private ClientsReservationModels GetClientsReservationModels()
    {
      IQueryable<ClientModels> clients = db.ClientModels;
      IQueryable<RoomModels> rooms = db.RoomModels;

      return new ClientsReservationModels
      {
        Clients = GetSelectClientListItems(clients),
        Rooms = GetSelectRoomListItems(rooms)
      };
    }
    
    private static IEnumerable<SelectListItem> GetSelectClientListItems(IEnumerable<ClientModels> elements)
    {
      var selectList = elements.Select(element => new SelectListItem
      {
        Value = element.Id.ToString(), Text = element.Name + " " + element.Surname
      }).ToList();
      return selectList;
    }

    private static IEnumerable<SelectListItem> GetSelectRoomListItems(IEnumerable<RoomModels> elements)
    {
      var selectList = elements.Select(element => new SelectListItem
      {
        Value = element.Id.ToString(),
        Text = element.RoomName
      }).ToList();
      return selectList;
    }
      // POST: ReservationModels/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]//[Bind(Include = "SelectedClientId,SelectedRoomId,ArriveDate")] 
    public async Task<ActionResult> Create([Bind(Include = "SelectedClientId, SelectedRoomId, Event")] ClientsReservationModels clientReservation)
    {
      var reservation = new ReservationModels();
      var events = new Event();
      try
      {
        reservation.Id = Guid.NewGuid();
        reservation.ClientId = clientReservation.SelectedClientId;

        events.Id = Guid.NewGuid();
        events.ArriveDate = clientReservation.Event.ArriveDate;
        events.DepatureDate = clientReservation.Event.DepatureDate;
        switch (events.ReservationState)
        {
          case ReservationStates.Pobyt: 
            events.RoomState = RoomStates.Zajęty;
            break;
          case ReservationStates.Zatwierdzona:
            events.RoomState = RoomStates.Zarezerowany;
            break;
          case ReservationStates.Niepotwierdzona: case ReservationStates.Odwołana: case ReservationStates.Zamknięta:
            events.RoomState = RoomStates.Wolny;
            break;
        }
        events.Reservation = new ReservationModels
        {
          Id = reservation.Id,
          ClientId = clientReservation.SelectedClientId
        };
        events.Room = db.RoomModels.FirstOrDefault(x => x.Id == clientReservation.SelectedRoomId);
        events.ReservationState = clientReservation.Event.ReservationState;
        events.Price = db.RoomModels.Where(x => x.Id == events.Room.Id).Select(x => x.FixedPricePerRoom).First();
        events.Reservation = reservation;
        db.Events.Add(events);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");

      }
      catch (Exception)
      {
        ViewBag.Id = new SelectList(db.Events, "Id", "Id", reservation.Id);
        return View(GetClientsReservationModels());
      }
      

     
    }

    // GET: ReservationModels/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var reservationModels = GetClientsReservationModels();
      reservationModels.Event = await db.Events.FindAsync(id);
      reservationModels.SelectedClientId = reservationModels.Event.Reservation.ClientId;
      reservationModels.SelectedRoomId = reservationModels.Event.Room.Id;

      ViewBag.Id = new SelectList(db.Events, "Id", "Id");
      return View(reservationModels);
    }

    // POST: ReservationModels/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,SelectedClientId, SelectedRoomId, Event, Reservation")] ClientsReservationModels reservationModels)
    {
      reservationModels.Event.Room = await db.RoomModels.FindAsync(reservationModels.SelectedRoomId);
      reservationModels.Event.Reservation =
        await db.ReservationModelses.FindAsync(reservationModels.Event.Reservation.Id);
      reservationModels.Event.Reservation.Client = await db.ClientModels.FindAsync(reservationModels.SelectedClientId);
      reservationModels.Event.Reservation.ClientId = reservationModels.SelectedClientId;
      reservationModels.Event.Price = reservationModels.Event.Reservation.Events.Select(x => x.Price).First();
      reservationModels.Clients = GetSelectClientListItems(db.ClientModels);
      reservationModels.Rooms = GetSelectRoomListItems(db.RoomModels);
      if (ModelState.IsValid)
      {
        db.Entry(reservationModels.Event).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      ViewBag.Id = new SelectList(db.Events, "Id", "Id", reservationModels.Event.Id);
      var clientReservationModel = GetClientsReservationModels();
      clientReservationModel.Event = await db.Events.FindAsync(reservationModels.Event.Id);
      clientReservationModel.SelectedClientId = reservationModels.SelectedClientId;
      clientReservationModel.SelectedRoomId = reservationModels.SelectedRoomId;
      return View(clientReservationModel);
    }

    // GET: ReservationModels/Delete/5
    public async Task<ActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Event @event = await db.Events.FindAsync(id);
      if (@event == null)
      {
        return HttpNotFound();
      }
      return View(@event);
    }

    // POST: ReservationModels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
      Event @event = await db.Events.FindAsync(id);
      db.Events.Remove(@event);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }



    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}