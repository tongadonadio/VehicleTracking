using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Repository
{
    public class RoleDAOImp : RoleDAO
    {
        public Role FindRole(Roles role)
        {
            Role databaseRole = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                string roleName = role.GetName();
                var query = from r in context.Roles
                            where r.Name == roleName
                            select r;
                databaseRole = query.ToList().FirstOrDefault();

                if(databaseRole == null)
                {
                    Role newRole = new Role(role);
                    context.Roles.Add(newRole);
                    context.SaveChanges();
                    databaseRole = newRole;
                }
            }

            return databaseRole;
        }
    }
}
