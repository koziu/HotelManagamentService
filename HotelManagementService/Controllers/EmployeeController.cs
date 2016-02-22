using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HotelManagementService.Attribute;
using HotelManagementService.Models;
using HotelManagementService.Models.Interfaces;

namespace HotelManagementService.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly ApplicationDbContext _appContext = new ApplicationDbContext();
    private readonly IEmployeesService _employeesService;

    public EmployeeController(IEmployeesService employeesService)
    {
      _employeesService = employeesService;
    }

    // GET: Employee
    [HotelManagamentAuthorize(Roles = "Administrator, Employee")]
    public ActionResult Index()
    {
      var registerViewModelList = new List<RegisterViewModel>();
      foreach (EmployeeModels employee in _employeesService.GetEmployees())
      {
        var registerViewModel = new RegisterViewModel();
        string userIdText = employee.UserId;
        ApplicationUser appUser = _appContext.Users.Find(userIdText);

        registerViewModel.Employee = employee;
        if (appUser != null) registerViewModel.UserName = appUser.UserName;
        registerViewModelList.Add(registerViewModel);
      }


      return View(registerViewModelList);
    }

    // GET: Employee/Details/5
    [HotelManagamentAuthorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      EmployeeModels employeeModels = _employeesService.GetEmployee(id);
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
    public ActionResult Create(
      [Bind(Include = "UserName, Password, ConfirmPassword,Employee")] RegisterViewModel registerViewModel,
      HttpPostedFileBase upload)
    {
      //if (!ModelState.IsValid) return View();
      using (var reader = new BinaryReader(upload.InputStream))
      {
        registerViewModel.Employee.ProfileImage = reader.ReadBytes(upload.ContentLength);
      }

      return RedirectToAction("Register", "Account", new {employeeModels = registerViewModel});
    }

    [HotelManagamentAuthorize(Roles = "Administrator")]
    // GET: Employee/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var registerViewModel = new RegisterViewModel {Employee = _employeesService.GetEmployee(id)};
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
      [Bind(Include = "Employee")] RegisterViewModel registerViewModel, HttpPostedFileBase upload)
    {
      //if (ModelState.IsValid)
      //{
      //zapisanie AppUSer
      using (var reader = new BinaryReader(upload.InputStream))
      {
        registerViewModel.Employee.ProfileImage = reader.ReadBytes(upload.ContentLength);
      }
      var id = _employeesService.GetEmployee(registerViewModel.Employee.Id).UserId;

      registerViewModel.Employee.UserId = id;
      _employeesService.EditEmployee(registerViewModel.Employee);
      return RedirectToAction("Index");
      //}
      return View(registerViewModel);
    }

    // GET: Employee/Delete/5
    [HotelManagamentAuthorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      EmployeeModels employeeModels = _employeesService.GetEmployee(id);
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
    public async Task<ActionResult> DeleteConfirmed(Guid? id)
    {
      EmployeeModels employeeModels = _employeesService.GetEmployee(id);
      _employeesService.DeleteEmployee(employeeModels);

      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _employeesService.Dispose();
        _appContext.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}