using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class FlowStepAlreadyExistException : Exception
    {
        public FlowStepAlreadyExistException() : base() { }
        public FlowStepAlreadyExistException(string message) : base(message) { }
        public FlowStepAlreadyExistException(string message, Exception inner) : base(message, inner) { }
    }
}
