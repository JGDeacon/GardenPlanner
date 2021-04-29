namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocialChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Likes", "UserID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Likes", "UserID");
        }
    }
}
