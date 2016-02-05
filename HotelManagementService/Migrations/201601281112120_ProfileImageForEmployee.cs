namespace HotelManagementService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileImageForEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "ProfileImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ProfileImage");
        }
    }
}
