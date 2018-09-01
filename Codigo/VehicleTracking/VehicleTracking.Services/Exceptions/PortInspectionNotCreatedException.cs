using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class PortInspectionNotCreatedException: Exception
    {
        public PortInspectionNotCreatedException() : base() { }
        public PortInspectionNotCreatedException(string message) : base(message) { }
        public PortInspectionNotCreatedException(string message, Exception inner) : base(message, inner) { }
    }
}
