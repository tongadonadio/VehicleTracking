using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class AssignVehicleToMainZoneException : Exception
    {
        public AssignVehicleToMainZoneException() : base() { }
        public AssignVehicleToMainZoneException(string message) : base(message) { }
        public AssignVehicleToMainZoneException(string message, Exception inner) : base(message, inner) { }

    }
}
