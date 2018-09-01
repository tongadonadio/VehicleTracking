using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ZoneCannotBeDeletedException : Exception
    {
        public ZoneCannotBeDeletedException() : base() { }
        public ZoneCannotBeDeletedException(string message) : base(message) { }
        public ZoneCannotBeDeletedException(string message, Exception inner) : base(message, inner) { }
    }
}
