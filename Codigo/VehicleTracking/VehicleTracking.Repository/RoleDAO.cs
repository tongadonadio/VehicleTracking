using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Repository
{
    public interface RoleDAO
    {
        Role FindRole(Roles role);
    }
}
