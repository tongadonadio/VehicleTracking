using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleTracking.Web.Models
{
    public class UserDTO : Model
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int Phone { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}