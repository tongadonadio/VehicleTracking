using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class AssignVehicleToZoneWithoutCapacityException : Exception
    {
        public AssignVehicleToZoneWithoutCapacityException() : base() { }
        public AssignVehicleToZoneWithoutCapacityException(string message) : base(message) { }
        public AssignVehicleToZoneWithoutCapacityException(string message, Exception inner) : base(message, inner) { }
    }
}
