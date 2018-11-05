namespace OverBevakningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Robots",
                c => new
                    {
                        RobId = c.Int(nullable: false, identity: true),
                        Beskrivning = c.String(),
                        IntPuls = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RobId);
            
            CreateTable(
                "dbo.RobotsLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        RobId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Robots", t => t.RobId)
                .Index(t => t.RobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RobotsLogs", "RobId", "dbo.Robots");
            DropIndex("dbo.RobotsLogs", new[] { "RobId" });
            DropTable("dbo.RobotsLogs");
            DropTable("dbo.Robots");
        }
    }
}
