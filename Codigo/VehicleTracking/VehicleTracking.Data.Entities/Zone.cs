using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Zone : Entity
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsSubZone { get; set; }

        public int MaxCapacity { get; set; }

        public virtual List<Zone> SubZones { get; set; }

        public virtual List<Vehicle> Vehicles { get; set; }

        public virtual FlowStep FlowStep { get; set; }

    }
}
