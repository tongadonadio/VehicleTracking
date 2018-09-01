using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class FlowItem
    {
        public Guid Id { get; set; }

        public virtual FlowStep FlowStep { get; set; }

        public int StepNumber { get; set; }
    }
}
