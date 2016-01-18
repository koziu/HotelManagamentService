namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomevent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Reservation", "ClientId", "dbo.Client");
            CreateTable(
                "dbo.RoomReservation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomState = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Room_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Room_Id);
            
            AddForeignKey("dbo.Events", "Room_Id", "dbo.Room", "Id");
            AddForeignKey("dbo.Reservation", "ClientId", "dbo.Client", "Id");
            DropColumn("dbo.Room", "RoomState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Room", "RoomState", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reservation", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.RoomReservation", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.RoomReservation", "Event_Id", "dbo.Events");
            DropIndex("dbo.RoomReservation", new[] { "Room_Id" });
            DropIndex("dbo.RoomReservation", new[] { "Event_Id" });
            DropTable("dbo.RoomReservation");
            AddForeignKey("dbo.Reservation", "ClientId", "dbo.Client", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Room_Id", "dbo.Room", "Id", cascadeDelete: true);
        }
    }
}
