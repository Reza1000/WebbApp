using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverBevakningApp.Models
{
    public class Robot
    {
        internal static string item;

        [Key]
        [Display(Name ="Robot ID")]
        public int RobId { get; set; }
        [Display(Name ="Robotnamn")]
        public string Beskrivning { get; set; }
        [Display(Name = "Intervall (minuter)")]
        public int IntPuls { get; set; }
        public bool MailSended { get; set; }
        [Display(Name = "Mejladress")]
        public string ContactInfo { get; set; }

        public ICollection<RobotsLog> RobotsLogs { get; set; }
      
    }
}