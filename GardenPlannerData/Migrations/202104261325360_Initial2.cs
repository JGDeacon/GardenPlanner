namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlantDetails", "PlantWidthMax", c => c.Double(nullable: false));
            DropColumn("dbo.PlantDetails", "PlantWidthtMax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlantDetails", "PlantWidthtMax", c => c.Double(nullable: false));
            DropColumn("dbo.PlantDetails", "PlantWidthMax");
        }
    }
}
