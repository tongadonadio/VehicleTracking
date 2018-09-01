using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class VehicleStatusDoesntAllowToAssignZoneException : Exception
    {
        public VehicleStatusDoesntAllowToAssignZoneException() : base() { }
        public VehicleStatusDoesntAllowToAssignZoneException(string message) : base(message) { }
        public VehicleStatusDoesntAllowToAssignZoneException(string message, Exception inner) : base(message, inner) { }
    }
}
