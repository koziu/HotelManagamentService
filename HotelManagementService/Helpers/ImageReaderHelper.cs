using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using HotelManagementService.Models;
using HotelManagementService.Models.EF;
using HotelManagementService.Models.Interfaces;
using HotelManagementService.Models.Service;

namespace HotelManagementService.Helpers
{
  public static class ImageReaderHelper
  {
    private static readonly IDataModelEF _context = new DataModelEF();


    public static byte[] GetPhoto(Guid? userId)
    {
      using (var db = _context.CreateNew())
      {
        var user = db.EmployeeModels.First(u => u.UserId == userId.ToString());

        if (user.ProfileImage != null)
        {
          var binaryImage = user.ProfileImage;
          return binaryImage;
        }
        return new byte[10];
      }

     
    }
  }
}