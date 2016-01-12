using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HotelManagementService.Attribute;
using HotelManagementService.DAL.Context;
using HotelManagementService.Models;
using HotelManagementService.ViewModels;

namespace HotelManagementService.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly HotelManagementContext _context = new HotelManagementContext();
    private readonly ApplicationDbContext _appContext = new ApplicationDbContext();

    // GET: Employee
    [HotelManagamentAuthorize(Roles = "Administrator, Employee")]
    public ActionResult Index()
    {
      var registerViewModelList = new List<RegisterViewModel>();
      foreach (var employee in _context.EmployeeModels.ToList())
      {
        var registerViewModel = new RegisterViewModel();     
        var userIdText = employee.UserId; 
        var appUser = _appContext.Users.Find(userIdText) as ApplicationUser;

        registerViewModel.Employee = employee;
        if (appUser != null) registerViewModel.UserName = appUser.UserName;    
        registerViewModelList.Add(registerViewModel);
      }


      return View(registerViewModelList);
    }

    // GET: Employee/Details/5
    [HotelManagamentAuthorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult> Details(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var employeeModels = await _context.EmployeeModels.FindAsync(id);
      if (employeeModels == null)
      {
        return HttpNotFound();
      }
      return View(employeeModels);
    }


    // GET: Employee/Create
    [HotelManagamentAuthorize(Roles = "Administrator")]
    public ActionResult Create()
    {
      return View();
    }

     //POST: Employee/Create
     //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
     //more details see http://go.microsoft.com/fwlink/?LinkId=317598.

    [HttpPost]
    [ValidateAntiForgeryToken]
    [HotelManagamentAuthorize(Roles = "Administrator")]
    public async Task<ActionResult> Create(
      [Bind(Include = "UserName, Password, ConfirmPassword,Employee")] RegisterViewModel registerViewModel)
    {

      return RedirectToAction("Register", "Account", new { employeeModels = registerViewModel });
    }

    [HotelManagamentAuthorize(Roles = "Administrator")]
    // GET: Employee/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      RegisterViewModel registerViewModel = new RegisterViewModel();
      registerViewModel.Employee  = await _context.EmployeeModels.FindAsync(id);
      if (registerViewModel.Employee == null)
      {
        return HttpNotFound();
      }
      return View(registerViewModel);
    }

    // POST: Employee/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HotelManagamentAuthorize(Roles = "Administrator")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(
      [Bind(Include = "Employee")] RegisterViewModel registerViewModel)
    {
      if (ModelState.IsValid)
      {
        //zapisanie AppUSer
        _context.Entry(registerViewModel.Employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(registerViewModel);
    }

    // GET: Employee/Delete/5
    [HotelManagamentAuthorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var employeeModels = await _context.EmployeeModels.FindAsync(id);
      if (employeeModels == null)
      {
        return HttpNotFound();
      }
      return View(employeeModels);
    }

    // POST: Employee/Delete/5
    [HotelManagamentAuthorize(Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
      var employeeModels = await _context.EmployeeModels.FindAsync(id);
      _context.EmployeeModels.Remove(employeeModels);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _context.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}