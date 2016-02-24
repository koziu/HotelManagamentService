using System.Data.Entity;
using HotelManagementService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementService.Models.Service
{
  public class EmployeesSerivce : Service, IEmployeesService
  {
    private readonly IDataModelEF _context ;

    public EmployeesSerivce(IDataModelEF context) : base(context)
    {
      _context = context;
    }

    public List<EmployeeModels> GetEmployees()
    {
      using (var db = _context.CreateNew())
      {
        return db.EmployeeModels.ToList();
      }
    }

    public EmployeeModels GetEmployee(Guid? employeeId)
    {
      using (var db = _context.CreateNew())
      {
        return db.EmployeeModels.Find(employeeId);
      }
    }

    public void DeleteEmployee(EmployeeModels employee)
    {
      using (var db = _context.CreateNew())
      {
        db.EmployeeModels.Attach(employee);
        db.EmployeeModels.Remove(employee);
        db.SaveChanges();
      }
    }

    public void AddEmployee(EmployeeModels employee)
    {
      using (var db = _context.CreateNew())
      {
        db.EmployeeModels.Add(employee);
        db.SaveChanges();
      }
    }

    public void EditEmployee(EmployeeModels employee)
    {
      using (var db = _context.CreateNew())
      {
        db.Entry(employee).State = EntityState.Modified;
        db.SaveChanges();
      }
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