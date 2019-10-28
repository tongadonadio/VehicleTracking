using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Log.Events
{
    public abstract class LogEvent
    {
        public abstract string Type { get; }
        public DateTime Date { get; set; }
        public UserDTO User { get; set; }
        public string Content { get; set; }

        public LogEvent(UserDTO user)
        {
            this.Date = DateTime.Now;
            this.User = user;
            this.Content = "";
        }
    }
}
