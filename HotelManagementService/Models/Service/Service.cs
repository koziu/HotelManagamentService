using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementService.Models.Interfaces;

namespace HotelManagementService.Models.Service
{
  public class Service : IService
  {
    private readonly IDataModelEF _context;

    public Service(IDataModelEF context)
    {
      _context = context;
    }
    public void Dispose()
    {
      using (var db = _context.CreateNew())
      {
        db.Dispose();
      }
    }
  }
}