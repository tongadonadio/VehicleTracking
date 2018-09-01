using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ZoneNullAttributesException : Exception
    {

        public ZoneNullAttributesException() : base() { }
        public ZoneNullAttributesException(string message) : base(message) { }
        public ZoneNullAttributesException(string message, Exception inner) : base(message, inner) { }

    }
}
