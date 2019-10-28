using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Log.Events
{
    public class VehicleImportEvent : LogEvent
    {
        public override string Type => "Vehicle Import";

        public VehicleImportEvent(UserDTO user, VehicleDTO vehicle) : base(user)
        {
            this.Content = "Se importó el vehículo: " + vehicle.Vin ;
        }
    }
}
