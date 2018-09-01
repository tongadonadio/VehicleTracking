using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Role : Entity
    {
        public Role()
        {
        }

        public Role (Roles role)
        {
            this.Name = role.GetName();
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
