using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.Services;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class RoleServiceImp : RoleService
    {
        public List<string> GetRoles()
        {
            List<string> roles = new List<string>();

            roles.Add(Roles.ADMINISTRATOR.GetName());
            roles.Add(Roles.CARRIER.GetName());
            roles.Add(Roles.PORT_OPERATOR.GetName());
            roles.Add(Roles.YARD_OPERATOR.GetName());
            roles.Add(Roles.SELLER.GetName());

            return roles;
        }
    }
}
