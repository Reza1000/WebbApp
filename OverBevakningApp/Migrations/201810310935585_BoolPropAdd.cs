namespace OverBevakningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolPropAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Robots", "MailSended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Robots", "MailSended");
        }
    }
}
