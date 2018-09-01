using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class UserLoggedDTO
    {
        public UserLoggedDTO() { }

        public Guid Token { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

    }
}
