namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Position");
        }
    }
}
