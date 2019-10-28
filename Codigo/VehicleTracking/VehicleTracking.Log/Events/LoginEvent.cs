using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Log.Events
{
    public class LoginEvent : LogEvent
    {
        public override string Type => "Login";
        
        public LoginEvent(UserDTO user) : base(user)
        {
            this.Content = "Se logueó el usuario: " + user.UserName;
        }
    }
}
