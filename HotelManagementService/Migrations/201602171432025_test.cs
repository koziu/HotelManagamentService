namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ArriveDate2", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "DepatureDate2", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "DepatureDate2");
            DropColumn("dbo.Events", "ArriveDate2");
        }
    }
}
