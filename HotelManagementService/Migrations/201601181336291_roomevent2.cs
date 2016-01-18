namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomevent2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomReservation", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.RoomReservation", "Room_Id", "dbo.Room");
            DropIndex("dbo.RoomReservation", new[] { "Event_Id" });
            DropIndex("dbo.RoomReservation", new[] { "Room_Id" });
            DropTable("dbo.RoomReservation");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomReservation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomState = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Room_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.RoomReservation", "Room_Id");
            CreateIndex("dbo.RoomReservation", "Event_Id");
            AddForeignKey("dbo.RoomReservation", "Room_Id", "dbo.Room", "Id");
            AddForeignKey("dbo.RoomReservation", "Event_Id", "dbo.Events", "Id");
        }
    }
}
