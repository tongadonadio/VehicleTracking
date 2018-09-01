using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class BatchNotFoundException : Exception
    {
        public BatchNotFoundException() : base() { }
        public BatchNotFoundException(string message) : base(message) { }
        public BatchNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
