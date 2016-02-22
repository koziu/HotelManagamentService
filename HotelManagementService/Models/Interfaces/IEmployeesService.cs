using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace HotelManagementService.Models.Interfaces
{
  public interface IEmployeesService
  {
    List<EmployeeModels> GetEmployees();
    EmployeeModels GetEmployee(Guid? employeeId);
    void DeleteEmployee(EmployeeModels employee);
    void AddEmployee(EmployeeModels employee);
    void EditEmployee(EmployeeModels employee);
    void Dispose();
  }
}
