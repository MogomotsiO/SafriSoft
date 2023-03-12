namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            //DropTable("dbo.Organisation");
            //DropTable("dbo.OrganisationSoftware");
            //DropTable("dbo.PackageFeature");
            //DropTable("dbo.Package");
            //DropTable("dbo.Software");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.Software",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Package",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            PackageName = c.String(),
            //            PackageDescription = c.String(),
            //            PackagePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            SoftwareId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.PackageFeature",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FeatureName = c.String(),
            //            FeatureDescription = c.String(),
            //            PackageId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.OrganisationSoftware",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            OrganisationId = c.Int(nullable: false),
            //            SoftwareId = c.Int(nullable: false),
            //            Granted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Organisation",
            //    c => new
            //        {
            //            OrganisationId = c.Int(nullable: false, identity: true),
            //            OrganisationName = c.String(),
            //            OrganisationEmail = c.String(),
            //            OrganisationCell = c.String(),
            //            OrganisationLogo = c.String(),
            //            OrganisationStreet = c.String(),
            //            OrganisationSuburb = c.String(),
            //            OrganisationCity = c.String(),
            //            OrganisationCode = c.Int(nullable: false),
            //            AccountName = c.String(),
            //            AccountNo = c.Int(nullable: false),
            //            BankName = c.String(),
            //            BranchName = c.String(),
            //            BranchCode = c.String(),
            //            ClientReference = c.String(),
            //            VATNumber = c.Int(nullable: false),
            //            ImgLogoSource = c.String(),
            //            PackageId = c.Int(nullable: false),
            //            OrganisationProvince = c.String(),
            //        })
            //    .PrimaryKey(t => t.OrganisationId);
            
            //DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            //DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            //DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            //DropIndex("dbo.AspNetUsers", "UserNameIndex");
            //DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            //DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetRoles");
        }
    }
}
