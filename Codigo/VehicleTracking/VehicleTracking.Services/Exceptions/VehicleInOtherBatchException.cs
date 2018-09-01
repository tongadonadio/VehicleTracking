using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class VehicleInOtherBatchException : Exception
    {
        public VehicleInOtherBatchException() : base() { }
        public VehicleInOtherBatchException(string message) : base(message) { }
        public VehicleInOtherBatchException(string message, Exception inner) : base(message, inner) { }
    }
}
