using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementService.DAL.Context;
using HotelManagementService.Models.Interfaces;

namespace HotelManagementService.Models.EF
{
  public class DataModelEF : IDataModelEF
  {
    public HotelManagementContext CreateNew()
    {
      return new HotelManagementContext();
    }
  }
}