namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailupdatev2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "EmailStatus", c => c.String());
            DropColumn("dbo.Emails", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "Status", c => c.String());
            DropColumn("dbo.Emails", "EmailStatus");
        }
    }
}
