using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class VehicleHistoryDTO
    {

        public List<HistoricVehicleDTO> VehicleHistory { get; set; }

        public List<InspectionDTO> InspectionHistory { get; set; }

    }
}
