using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class ZoneDTO : Model
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsSubZone { get; set; }

        public int MaxCapacity { get; set; }

        public List<ZoneDTO> SubZones { get; set; }

        public List<VehicleDTO> Vehicles { get; set; }

        public FlowStepDTO FlowStep { get; set; }

    }
}
