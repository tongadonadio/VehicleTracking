using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class VehicleNullAttributesException : Exception
    {
        public VehicleNullAttributesException() : base() { }
        public VehicleNullAttributesException(string message) : base(message) { }
        public VehicleNullAttributesException(string message, Exception inner) : base(message, inner) { }
    }
}
