using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ZoneNotFoundException : Exception
    {

        public ZoneNotFoundException() : base() { }
        public ZoneNotFoundException(string message) : base(message) { }
        public ZoneNotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
