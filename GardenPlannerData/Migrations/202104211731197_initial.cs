namespace GardenPlannerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyPlants",
                c => new
                    {
                        MyPlantID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        Location = c.String(maxLength: 500),
                        PlantID = c.Int(nullable: false),
                        DatePlanted = c.DateTimeOffset(nullable: false, precision: 7),
                        Photo = c.String(),
                        Notes = c.String(maxLength: 3000),
                        Year = c.Int(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.MyPlantID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        PlantID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ScientificName = c.String(nullable: false),
                        ZoneID = c.Int(nullable: false),
                        SeasonID = c.Int(nullable: false),
                        PlantTypeID = c.Int(nullable: false),
                        PlantCareID = c.Int(nullable: false),
                        PlantDetailsID = c.Int(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.PlantID)
                .ForeignKey("dbo.PlantCare", t => t.PlantCareID, cascadeDelete: true)
                .ForeignKey("dbo.PlantDetails", t => t.PlantDetailsID, cascadeDelete: true)
                .ForeignKey("dbo.PlantSeasons", t => t.SeasonID, cascadeDelete: true)
                .ForeignKey("dbo.PlantTypes", t => t.PlantTypeID, cascadeDelete: true)
                .ForeignKey("dbo.PlantZones", t => t.ZoneID, cascadeDelete: true)
                .Index(t => t.ZoneID)
                .Index(t => t.SeasonID)
                .Index(t => t.PlantTypeID)
                .Index(t => t.PlantCareID)
                .Index(t => t.PlantDetailsID);
            
            CreateTable(
                "dbo.PlantCare",
                c => new
                    {
                        PlantCareID = c.Int(nullable: false, identity: true),
                        SunExposureID = c.Int(nullable: false),
                        WaterNeedID = c.Int(nullable: false),
                        Temperature = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.PlantCareID)
                .ForeignKey("dbo.SunExposures", t => t.SunExposureID, cascadeDelete: true)
                .ForeignKey("dbo.WaterNeeds", t => t.WaterNeedID, cascadeDelete: true)
                .Index(t => t.SunExposureID)
                .Index(t => t.WaterNeedID);
            
            CreateTable(
                "dbo.SunExposures",
                c => new
                    {
                        SunExposureID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.SunExposureID);
            
            CreateTable(
                "dbo.WaterNeeds",
                c => new
                    {
                        WaterNeedID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.WaterNeedID);
            
            CreateTable(
                "dbo.PlantDetails",
                c => new
                    {
                        PlantDetailsID = c.Int(nullable: false, identity: true),
                        DaysToGerminate = c.Int(nullable: false),
                        DaysToHarvest = c.Int(nullable: false),
                        SeedDepth = c.Double(nullable: false),
                        IsPerennial = c.Boolean(nullable: false),
                        PlantHeightMax = c.Double(nullable: false),
                        PlantWidthtMax = c.Double(nullable: false),
                        SeedSpacing = c.Double(nullable: false),
                        RowSpacing = c.Double(nullable: false),
                        RootStructureID = c.Int(nullable: false),
                        IsDeerResistant = c.Boolean(nullable: false),
                        IsToxicToAnimal = c.Boolean(nullable: false),
                        IsToxicToHuman = c.Boolean(nullable: false),
                        IsMedicinal = c.Boolean(nullable: false),
                        Image = c.String(nullable: false, maxLength: 3000),
                        Description = c.String(),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.PlantDetailsID)
                .ForeignKey("dbo.RootStructure", t => t.RootStructureID, cascadeDelete: true)
                .Index(t => t.RootStructureID);
            
            CreateTable(
                "dbo.RootStructure",
                c => new
                    {
                        RootStructureID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.RootStructureID);
            
            CreateTable(
                "dbo.PlantSeasons",
                c => new
                    {
                        SeasonID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.SeasonID);
            
            CreateTable(
                "dbo.PlantTypes",
                c => new
                    {
                        PlantTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.PlantTypeID);
            
            CreateTable(
                "dbo.PlantZones",
                c => new
                    {
                        ZoneID = c.Int(nullable: false, identity: true),
                        ZoneCode = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Modifiedate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ZoneID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.MyPlants", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.Plants", "ZoneID", "dbo.PlantZones");
            DropForeignKey("dbo.Plants", "PlantTypeID", "dbo.PlantTypes");
            DropForeignKey("dbo.Plants", "SeasonID", "dbo.PlantSeasons");
            DropForeignKey("dbo.Plants", "PlantDetailsID", "dbo.PlantDetails");
            DropForeignKey("dbo.PlantDetails", "RootStructureID", "dbo.RootStructure");
            DropForeignKey("dbo.Plants", "PlantCareID", "dbo.PlantCare");
            DropForeignKey("dbo.PlantCare", "WaterNeedID", "dbo.WaterNeeds");
            DropForeignKey("dbo.PlantCare", "SunExposureID", "dbo.SunExposures");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.PlantDetails", new[] { "RootStructureID" });
            DropIndex("dbo.PlantCare", new[] { "WaterNeedID" });
            DropIndex("dbo.PlantCare", new[] { "SunExposureID" });
            DropIndex("dbo.Plants", new[] { "PlantDetailsID" });
            DropIndex("dbo.Plants", new[] { "PlantCareID" });
            DropIndex("dbo.Plants", new[] { "PlantTypeID" });
            DropIndex("dbo.Plants", new[] { "SeasonID" });
            DropIndex("dbo.Plants", new[] { "ZoneID" });
            DropIndex("dbo.MyPlants", new[] { "PlantID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.PlantZones");
            DropTable("dbo.PlantTypes");
            DropTable("dbo.PlantSeasons");
            DropTable("dbo.RootStructure");
            DropTable("dbo.PlantDetails");
            DropTable("dbo.WaterNeeds");
            DropTable("dbo.SunExposures");
            DropTable("dbo.PlantCare");
            DropTable("dbo.Plants");
            DropTable("dbo.MyPlants");
        }
    }
}
