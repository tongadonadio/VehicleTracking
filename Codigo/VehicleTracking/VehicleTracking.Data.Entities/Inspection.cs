using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Inspection : Entity
    {
        public Inspection()
        {
        }

        public Guid Id { get; set; }

        public virtual List<Damage> Damages { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        public virtual Location IdLocation { get; set; }

        public virtual User IdUser { get; set; }

        public virtual Vehicle IdVehicle { get; set; }
    }
}
