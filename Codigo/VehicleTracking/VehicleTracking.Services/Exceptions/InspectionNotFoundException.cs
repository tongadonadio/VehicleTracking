using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class InspectionNotFoundException : Exception
    {
        public InspectionNotFoundException() : base() { }
        public InspectionNotFoundException(string message) : base(message) { }
        public InspectionNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
