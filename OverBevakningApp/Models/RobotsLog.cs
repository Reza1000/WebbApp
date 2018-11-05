using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverBevakningApp.Models
{
    public class RobotsLog
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Senaste tidsstämpel")]
        public DateTime TimeStamp { get; set; }
        public int? RobId { get; set; }

        //NavigationProperty
        public virtual Robot Rob { get; set; }
        
    }
}