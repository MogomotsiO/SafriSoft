namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SafriSoft : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Organisation", newName: "Organisations");
            RenameTable(name: "dbo.OrganisationSoftware", newName: "OrganisationSoftwares");
            RenameTable(name: "dbo.PackageFeature", newName: "PackageFeatures");
            RenameTable(name: "dbo.Package", newName: "Packages");
            RenameTable(name: "dbo.Software", newName: "Softwares");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Softwares", newName: "Software");
            RenameTable(name: "dbo.Packages", newName: "Package");
            RenameTable(name: "dbo.PackageFeatures", newName: "PackageFeature");
            RenameTable(name: "dbo.OrganisationSoftwares", newName: "OrganisationSoftware");
            RenameTable(name: "dbo.Organisations", newName: "Organisation");
        }
    }
}
