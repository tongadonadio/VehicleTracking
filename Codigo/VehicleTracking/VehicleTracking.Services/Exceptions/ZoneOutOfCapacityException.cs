using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ZoneOutOfCapacityException : Exception
    {
        public ZoneOutOfCapacityException() : base() { }
        public ZoneOutOfCapacityException(string message) : base(message) { }
        public ZoneOutOfCapacityException(string message, Exception inner) : base(message, inner) { }
    }
}
