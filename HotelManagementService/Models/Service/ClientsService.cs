using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using HotelManagementService.Models.Interfaces;
using HotelManagementService.ViewModels;

namespace HotelManagementService.Models.Service
{
  public class ClientsService : Service, IClientsService
  {
    private const string GetEventsByClientIdQueryText =
     @"SELECT e.Price, e.ReservationState, e.ArriveDate, e.DepatureDate FROM dbo.Events e join dbo.Reservation r on r.Id = e.Reservation_Id where r.ClientId = @p0";

    private readonly IDataModelEF _context;

    public ClientsService(IDataModelEF context) : base(context)
    {
      _context = context;
    }

    public List<ClientModels> GetClients()
    {
      using (var db = _context.CreateNew())
      {
        return db.ClientModels.ToList();
      }
    }

    public ClientModels GetClient(Guid? clientId)
    {
      using (var db = _context.CreateNew())
      {
        return db.ClientModels.Find(clientId);
      }
    }

    public void DeleteClient(ClientModels client)
    {
      using (var db = _context.CreateNew())
      {
        db.ClientModels.Attach(client);
        db.ClientModels.Remove(client);
        db.SaveChanges();
      }
    }

    public void AddClient(ClientModels client)
    {
      using (var db = _context.CreateNew())
      {
        client.Id = Guid.NewGuid();
        db.ClientModels.Add(client);
        db.SaveChanges();
      }
    }

    public void EditClient(ClientModels client)
    {
      using (var db = _context.CreateNew())
      {
        db.Entry(client).State = EntityState.Modified;
        db.SaveChanges();
      }
    }

    public DbRawSqlQuery<T> GetEventByClientId<T>(Guid? id)
    {
      using (var db = _context.CreateNew())
      {
        return db.Database.SqlQuery<T>(GetEventsByClientIdQueryText, id);
      }
    }
  }
}