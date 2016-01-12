using System;
using System.Data.Entity;
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
    private readonly HotelManagementContext db = new HotelManagementContext();

    // GET: Employee
    [NonAuthorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult> Index()
    {
      return View(await db.EmployeeModels.ToListAsync());
    }

    // GET: Employee/Details/5
    [NonAuthorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult> Details(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var employeeModels = await db.EmployeeModels.FindAsync(id);
      if (employeeModels == null)
      {
        return HttpNotFound();
      }
      return View(employeeModels);
    }


    // GET: Employee/Create
    [NonAuthorize(Roles = "Administrator")]
    public ActionResult Create()
    {
      return View();
    }

     //POST: Employee/Create
     //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
     //more details see http://go.microsoft.com/fwlink/?LinkId=317598.

    [HttpPost]
    [ValidateAntiForgeryToken]
    [NonAuthorize(Roles = "Administrator")]
    public async Task<ActionResult> Create(
      [Bind(Include = "UserName, Password, ConfirmPassword,Employee")] RegisterViewModel registerViewModel)
    {

      return RedirectToAction("Register", "Account", new { employeeModels = registerViewModel });
    }

    [NonAuthorize(Roles = "Administrator")]
    // GET: Employee/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      RegisterViewModel registerViewModel = new RegisterViewModel();
      registerViewModel.Employee  = await db.EmployeeModels.FindAsync(id);
      if (registerViewModel.Employee == null)
      {
        return HttpNotFound();
      }
      return View(registerViewModel);
    }

    // POST: Employee/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [NonAuthorize(Roles = "Administrator")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(
      [Bind(Include = "UserName, Password, ConfirmPassword,Employee")] RegisterViewModel registerViewModel)
    {
      if (ModelState.IsValid)
      {
        db.Entry(registerViewModel.Employee).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(registerViewModel);
    }

    // GET: Employee/Delete/5
    [NonAuthorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(Guid id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var employeeModels = await db.EmployeeModels.FindAsync(id);
      if (employeeModels == null)
      {
        return HttpNotFound();
      }
      return View(employeeModels);
    }

    // POST: Employee/Delete/5
    [NonAuthorize(Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
      var employeeModels = await db.EmployeeModels.FindAsync(id);
      db.EmployeeModels.Remove(employeeModels);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}