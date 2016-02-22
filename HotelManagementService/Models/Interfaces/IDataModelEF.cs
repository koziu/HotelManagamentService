using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementService.DAL.Context;

namespace HotelManagementService.Models.Interfaces
{
  public interface IDataModelEF
  {
    HotelManagementContext CreateNew();
  }
}
