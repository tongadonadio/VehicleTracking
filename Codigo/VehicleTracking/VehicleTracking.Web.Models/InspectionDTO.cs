using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class InspectionDTO : Model
    {

        public InspectionDTO()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public List<DamageDTO> Damages { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string CreatorUserName { get; set; }

        public string IdVehicle { get; set; }

    }
}
