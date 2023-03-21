namespace SafriSoftv1._3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "ReceiverToAddress", c => c.String());
            AddColumn("dbo.Emails", "ReceiverCCAddress", c => c.String());
            DropColumn("dbo.Emails", "ReceiverAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "ReceiverAddress", c => c.String());
            DropColumn("dbo.Emails", "ReceiverCCAddress");
            DropColumn("dbo.Emails", "ReceiverToAddress");
        }
    }
}
