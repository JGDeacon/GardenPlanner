namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MyPlants", "UserID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MyPlants", "UserID", c => c.Int(nullable: false));
        }
    }
}
