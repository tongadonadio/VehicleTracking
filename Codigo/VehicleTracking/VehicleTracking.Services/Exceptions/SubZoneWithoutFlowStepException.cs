using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class SubZoneWithoutFlowStepException : Exception
    {
        public SubZoneWithoutFlowStepException() : base() { }
        public SubZoneWithoutFlowStepException(string message) : base(message) { }
        public SubZoneWithoutFlowStepException(string message, Exception inner) : base(message, inner) { }

    }
}
