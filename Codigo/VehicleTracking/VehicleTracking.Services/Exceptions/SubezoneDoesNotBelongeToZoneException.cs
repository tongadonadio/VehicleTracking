using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class SubezoneDoesNotBelongeToZoneException : Exception
    {

        public SubezoneDoesNotBelongeToZoneException() : base() { }
        public SubezoneDoesNotBelongeToZoneException(string message) : base(message) { }
        public SubezoneDoesNotBelongeToZoneException(string message, Exception inner) : base(message, inner) { }

    }
}
