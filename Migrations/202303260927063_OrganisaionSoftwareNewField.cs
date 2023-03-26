namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisaionSoftwareNewField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganisationSoftwares", "PackageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganisationSoftwares", "PackageId");
        }
    }
}
