namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        Answer = c.String(nullable: false, maxLength: 3000),
                        UserID = c.Guid(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        PlantID = c.Int(nullable: false),
                        Question = c.String(nullable: false, maxLength: 1000),
                        UserID = c.Guid(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PlantID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(nullable: false, maxLength: 3000),
                        UserID = c.Guid(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeID = c.Int(nullable: false, identity: true),
                        IsLiked = c.Boolean(nullable: false),
                        PlantID = c.Int(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.LikeID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.RoleID);
            
            AddColumn("dbo.ApplicationUser", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUser", "PlantAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "PlantBloom", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "WhenToPlant", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "WhenToWater", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "Answer", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ApplicationUser", "ModifiedDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.Comments", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "PlantID", "dbo.Plants");
            DropIndex("dbo.Likes", new[] { "PlantID" });
            DropIndex("dbo.Comments", new[] { "PlantID" });
            DropIndex("dbo.Questions", new[] { "PlantID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropColumn("dbo.ApplicationUser", "ModifiedDate");
            DropColumn("dbo.ApplicationUser", "CreatedDate");
            DropColumn("dbo.ApplicationUser", "Answer");
            DropColumn("dbo.ApplicationUser", "WhenToWater");
            DropColumn("dbo.ApplicationUser", "WhenToPlant");
            DropColumn("dbo.ApplicationUser", "PlantBloom");
            DropColumn("dbo.ApplicationUser", "PlantAdded");
            DropColumn("dbo.ApplicationUser", "RoleID");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
