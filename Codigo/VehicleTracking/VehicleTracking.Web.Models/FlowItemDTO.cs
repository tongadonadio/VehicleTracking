using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class FlowItemDTO
    {
        public Guid Id { get; set; }

        public FlowStepDTO FlowStep { get; set; }

        public int StepNumber { get; set; }
    }
}
