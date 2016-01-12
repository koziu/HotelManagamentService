namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeUserId : DbMigration
    {
        public override void Up()
        {

          DropTable("dbo.Employee");
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        DeliveriesAddress = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        BrithDay = c.DateTime(nullable: false),
                        BrithPlace = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        TaxId = c.String(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
          
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.RoomElementModelsRoomModels", "RoomModels_Id", "dbo.Room");
            DropForeignKey("dbo.RoomElementModelsRoomModels", "RoomElementModels_Id", "dbo.RoomElement");
            DropForeignKey("dbo.Events", "Reservation_Id", "dbo.Reservation");
            DropForeignKey("dbo.Reservation", "ClientId", "dbo.Client");
            DropIndex("dbo.RoomElementModelsRoomModels", new[] { "RoomModels_Id" });
            DropIndex("dbo.RoomElementModelsRoomModels", new[] { "RoomElementModels_Id" });
            DropIndex("dbo.Reservation", new[] { "ClientId" });
            DropIndex("dbo.Events", new[] { "Room_Id" });
            DropIndex("dbo.Events", new[] { "Reservation_Id" });
            DropTable("dbo.RoomElementModelsRoomModels");
            DropTable("dbo.RoomElement");
            DropTable("dbo.Room");
            DropTable("dbo.Reservation");
            DropTable("dbo.Events");
            DropTable("dbo.Employee");
            DropTable("dbo.Client");
        }
    }
}
