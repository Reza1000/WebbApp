namespace OverBevakningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Robots", "ContactInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Robots", "ContactInfo");
        }
    }
}
