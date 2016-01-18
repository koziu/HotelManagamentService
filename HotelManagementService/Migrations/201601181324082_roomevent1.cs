namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomevent1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Reservation", "ClientId", "dbo.Client");
            AddColumn("dbo.Events", "RoomState", c => c.Int(nullable: false));
            AddForeignKey("dbo.Events", "Room_Id", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservation", "ClientId", "dbo.Client", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Room");
            DropColumn("dbo.Events", "RoomState");
            AddForeignKey("dbo.Reservation", "ClientId", "dbo.Client", "Id");
            AddForeignKey("dbo.Events", "Room_Id", "dbo.Room", "Id");
        }
    }
}
