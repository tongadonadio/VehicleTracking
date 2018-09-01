using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class IncorrectCreateFlowRequestException : Exception
    {
        public IncorrectCreateFlowRequestException() : base() { }
        public IncorrectCreateFlowRequestException(string message) : base(message) { }
        public IncorrectCreateFlowRequestException(string message, Exception inner) : base(message, inner) { }
    }
}
