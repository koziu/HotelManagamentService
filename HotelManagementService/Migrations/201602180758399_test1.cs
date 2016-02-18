namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "ArriveDate2");
            DropColumn("dbo.Events", "DepatureDate2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "DepatureDate2", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "ArriveDate2", c => c.DateTime(nullable: false));
        }
    }
}
