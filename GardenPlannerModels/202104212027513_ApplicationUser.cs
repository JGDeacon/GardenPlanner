namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.ApplicationUser", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUser", "PlantAdded", c => c.Boolean(nullable: true));
            AddColumn("dbo.ApplicationUser", "PlantBloom", c => c.Boolean(nullable: true));
            AddColumn("dbo.ApplicationUser", "WhenToPlant", c => c.Boolean(nullable: true));
            AddColumn("dbo.ApplicationUser", "WhenToWater", c => c.Boolean(nullable: true));
            AddColumn("dbo.ApplicationUser", "Answer", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
