using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace HotelManagementService.Models.Interfaces
{
  public interface IClientsService
  {
    List<ClientModels> GetClients();
    ClientModels GetClient(Guid? clientId);
    void DeleteClient(ClientModels client);
    void AddClient(ClientModels client);
    void EditClient(ClientModels client);
    DbRawSqlQuery<T> GetEventByClientId<T>(Guid? id);
    void Dispose();
  }
}