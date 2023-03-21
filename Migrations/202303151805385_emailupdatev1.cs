namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailupdatev1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "FromAddress", c => c.String());
            AddColumn("dbo.Emails", "ToAddress", c => c.String());
            AddColumn("dbo.Emails", "CcAddress", c => c.String());
            AddColumn("dbo.Emails", "Subject", c => c.String());
            AddColumn("dbo.Emails", "Body", c => c.String());
            DropColumn("dbo.Emails", "SenderAddress");
            DropColumn("dbo.Emails", "ReceiverToAddress");
            DropColumn("dbo.Emails", "ReceiverCCAddress");
            DropColumn("dbo.Emails", "Contents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "Contents", c => c.String());
            AddColumn("dbo.Emails", "ReceiverCCAddress", c => c.String());
            AddColumn("dbo.Emails", "ReceiverToAddress", c => c.String());
            AddColumn("dbo.Emails", "SenderAddress", c => c.String());
            DropColumn("dbo.Emails", "Body");
            DropColumn("dbo.Emails", "Subject");
            DropColumn("dbo.Emails", "CcAddress");
            DropColumn("dbo.Emails", "ToAddress");
            DropColumn("dbo.Emails", "FromAddress");
        }
    }
}
