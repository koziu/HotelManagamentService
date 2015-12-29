using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelManagementService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelManagementService.Models.ApplicationDbContext context)
        {
          IdentityRole administrator = new IdentityRole("Administrator");
          IdentityRole employee = new IdentityRole("Emoployee");
          context.Roles.Add(administrator);
          context.Roles.Add(employee);
          context.SaveChanges();
        }
    }
}
