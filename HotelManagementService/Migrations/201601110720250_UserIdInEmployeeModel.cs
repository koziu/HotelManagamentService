using System.Data.Entity.Migrations;

namespace HotelManagementService.Migrations
{
  public partial class UserIdInEmployeeModel : DbMigration
  {
    public override void Up()
    {
      DropTable("dbo.Employee");

      CreateTable(
        "dbo.Employee",
        c => new
        {
          Id = c.Guid(false),
          Name = c.String(false),
          Surname = c.String(false),
          Address = c.String(false),
          DeliveriesAddress = c.String(false),
          Email = c.String(),
          PhoneNumber = c.String(false),
          BrithDay = c.DateTime(false),
          BrithPlace = c.String(false),
          TaxId = c.String(false),
          UserId = c.Guid(false),
        })
        .PrimaryKey(t => t.Id);
      AddForeignKey("dbo.Employee", "UserId", "dbo.AspNetUsers", "Id", true);
    }

    public override void Down()
    {
     
    }
  }
}