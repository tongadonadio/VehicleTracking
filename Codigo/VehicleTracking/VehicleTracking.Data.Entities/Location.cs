using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Location : Entity
    {
        public Location() { }

        public Location(Locations locations)
        {
            this.Id = Guid.NewGuid();
            this.Name = locations.GetName();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
