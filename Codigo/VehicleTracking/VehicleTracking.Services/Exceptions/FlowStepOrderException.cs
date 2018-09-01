using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class FlowStepOrderException : Exception
    {

        public FlowStepOrderException() : base() { }
        public FlowStepOrderException(string message) : base(message) { }
        public FlowStepOrderException(string message, Exception inner) : base(message, inner) { }

    }
}
