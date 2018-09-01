using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Batch : Entity
    {
        public Batch()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual User IdUser { get; set; }
        
        public string Description { get; set; }
        
        public virtual List<Vehicle> Vehicles { get; set; } 

    }
}
