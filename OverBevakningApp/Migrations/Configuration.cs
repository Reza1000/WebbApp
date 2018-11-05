namespace OverBevakningApp.Migrations
{
    using OverBevakningApp.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    internal sealed class Configuration : DbMigrationsConfiguration<OverBevakningApp.DAL.RobotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OverBevakningApp.DAL.RobotContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //List<Robot> robot = new List<Robot>();

            ////robot.Add(new Robot() { RobId = 1, Beskrivning = "Stockholm" });
            ////robot.Add(new Robot() { RobId = 2, Beskrivning = "Karlstad" });

            //context.Robots.AddRange(robot);

            //base.Seed(context);
        }
    }
}
