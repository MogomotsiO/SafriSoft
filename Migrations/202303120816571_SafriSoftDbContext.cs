namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SafriSoftDbContext : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PackageFeature");

            CreateTable(
                "dbo.PackageFeature",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FeatureName = c.String(),
                    FeatureDescription = c.String(),
                    Limit = c.Int(nullable: false),
                    Granted = c.Boolean(nullable: false),
                    PackageId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.PackageFeature");
        }
    }
}
