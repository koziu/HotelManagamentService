using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementService.ViewModels
{
  public class RoleUsersViewModel
  {
    public string RoleName { get; set; }
    public IEnumerable<string> UserNameList { get; set; } 
  }
}