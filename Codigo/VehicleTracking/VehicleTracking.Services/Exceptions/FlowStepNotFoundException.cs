using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class FlowStepNotFoundException : Exception
    {
        public FlowStepNotFoundException() : base() { }
        public FlowStepNotFoundException(string message) : base(message) { }
        public FlowStepNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
