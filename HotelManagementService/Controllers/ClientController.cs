using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HotelManagementService.Attribute;
using HotelManagementService.DAL.Context;
using HotelManagementService.Models;
using HotelManagementService.Models.Interfaces;
using HotelManagementService.ViewModels;

namespace HotelManagementService.Controllers
{
  [HotelManagamentAuthorize(Roles = "Employee, Administrator")]
  public class ClientController : Controller
  {
   
    private readonly IClientsService _client;

    public ClientController()
    {
    }

    public ClientController(IClientsService client)
    {
      _client = client;
    }

    // GET: ClientModels
    public async Task<ActionResult> Index()
    {
      return View(_client.GetClients());
    }

    // GET: ClientModels/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ClientModels clientModels = _client.GetClient(id);
      if (clientModels == null)
      {
        return HttpNotFound();
      }
      return View(clientModels);
    }

    // GET: ClientModels/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: ClientModels/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(
      [Bind(
        Include =
          "Id,PhoneNumber,Street, City, PostalCode,Email,Comments,Name,Surname,IdentityCardNumber,BrithDay,BrithPlace,CompanyName,TaxId,REGON"
        )] ClientModels clientModels)
    {
      if (ModelState.IsValid)
      {
        _client.AddClient(clientModels);
        return RedirectToAction("Index");
      }

      return View(clientModels);
    }

    // GET: ClientModels/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ClientModels clientModels = _client.GetClient(id);
      if (clientModels == null)
      {
        return HttpNotFound();
      }
      return View(clientModels);
    }

    // POST: ClientModels/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(
      [Bind(
        Include =
          "Id,PhoneNumber,Street, City, PostalCode,Email,Comments,Name,Surname,IdentityCardNumber,BrithDay,BrithPlace,CompanyName,TaxId,REGON"
        )] ClientModels clientModels)
    {
      if (ModelState.IsValid)
      {
        _client.EditClient(clientModels);
        return RedirectToAction("Index");
      }
      return View(clientModels);
    }

    // GET: ClientModels/Delete/5
    public async Task<ActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ClientModels clientModels = _client.GetClient(id);
      if (clientModels == null)
      {
        return HttpNotFound();
      }
      return View(clientModels);
    }

    // POST: ClientModels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
      var clientModels = _client.GetClient(id);
      _client.DeleteClient(clientModels);
      return RedirectToAction("Index");
    }

    [HttpGet, ActionName("ClientReservation")]
    public async Task<ActionResult> GetEventsByClientId(Guid? id)
    {
      DbRawSqlQuery<EventViewModel> events = _client.GetEventByClientId<EventViewModel>(id);

      return View(events);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _client.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}