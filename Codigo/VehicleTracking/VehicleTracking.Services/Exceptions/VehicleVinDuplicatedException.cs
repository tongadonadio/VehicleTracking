using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class VehicleVinDuplicatedException : Exception
    {

        public VehicleVinDuplicatedException() : base() { }
        public VehicleVinDuplicatedException(string message) : base(message) { }
        public VehicleVinDuplicatedException(string message, Exception inner) : base(message, inner) { }

    }
}
