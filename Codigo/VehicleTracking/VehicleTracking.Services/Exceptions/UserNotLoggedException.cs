using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class UserNotLoggedException : Exception
    {
        public UserNotLoggedException() : base() { }
        public UserNotLoggedException(string message) : base(message) { }
        public UserNotLoggedException(string message, Exception inner) : base(message, inner) { }
    }
}
