using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class BatchIsNotReadyException : Exception
    {
        public BatchIsNotReadyException() : base() { }
        public BatchIsNotReadyException(string message) : base(message) { }
        public BatchIsNotReadyException(string message, Exception inner) : base(message, inner) { }
    }
}
