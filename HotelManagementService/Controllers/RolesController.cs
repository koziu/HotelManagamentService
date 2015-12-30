using System;
using System.Linq;
using System.Web.Mvc;
using HotelManagementService.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementService.Controllers
{
  [Authorize(Roles = "Administrator, Manager")]
  public class RolesController : Controller
  {
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public ActionResult ManageUserRoles()
    {
      // prepopulat roles for the view dropdown
      var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      ViewBag.Roles = list;
      return View();
    }
    public ActionResult Index()
    {
      var roles = _context.Roles.ToList();
      return View(roles);
    }

    public ActionResult Delete(string RoleName)
    {
      var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));
      _context.Roles.Remove(thisRole);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

   
    // GET: /Roles/Create
    public ActionResult Create()
    {
      return View();
    }

    //
    // POST: /Roles/Create
    [HttpPost]
    public ActionResult Create(FormCollection collection)
    {
      try
      {
        _context.Roles.Add(new IdentityRole
        {
          Name = collection["RoleName"]
        });
        _context.SaveChanges();
        ViewBag.ResultMessage = "Role created successfully !";
        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    //
    // GET: /Roles/Edit/5
    public ActionResult Edit(string roleName)
    {
      var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

      return View(thisRole);
    }

    //
    // POST: /Roles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
    {
      try
      {
        _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
        _context.SaveChanges();

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RoleAddToUser(string UserName, string RoleName)
    {

      ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
      var account = new AccountController();
      account.UserManager.AddToRole(user.Id, RoleName);

      ViewBag.ResultMessage = "Role created successfully !";

      // prepopulat roles for the view dropdown
      var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      ViewBag.Roles = list;

      return View("ManageUserRoles");
    }

    [ValidateAntiForgeryToken]
    public ActionResult GetRoles(string UserName)
    {
      if (!string.IsNullOrWhiteSpace(UserName))
      {
        ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
        var account = new AccountController();

        ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

        // prepopulat roles for the view dropdown
        var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        ViewBag.Roles = list;
      }

      return View("ManageUserRoles");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteRoleForUser(string UserName, string RoleName)
    {
      var account = new AccountController();
      ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

      if (account.UserManager.IsInRole(user.Id, RoleName))
      {
        account.UserManager.RemoveFromRole(user.Id, RoleName);
        ViewBag.ResultMessage = "Role removed from this user successfully !";
      }
      else
      {
        ViewBag.ResultMessage = "This user doesn't belong to selected role.";
      }
      // prepopulat roles for the view dropdown
      var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      ViewBag.Roles = list;

      return View("ManageUserRoles");
    }
  }
}