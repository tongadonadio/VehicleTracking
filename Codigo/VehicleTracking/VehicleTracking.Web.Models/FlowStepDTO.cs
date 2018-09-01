using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class FlowStepDTO
    {

        public FlowStepDTO(string name)
        {
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
