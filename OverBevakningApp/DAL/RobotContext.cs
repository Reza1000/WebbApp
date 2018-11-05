using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OverBevakningApp.Models;

namespace OverBevakningApp.DAL
{
    public class RobotContext : DbContext
    {
        public DbSet<RobotsLog> RobotsLogs { get; set; }
        public DbSet<Robot> Robots { get; set; }
    }
}