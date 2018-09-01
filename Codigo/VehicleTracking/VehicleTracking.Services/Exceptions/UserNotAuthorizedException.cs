using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException() : base() { }
        public UserNotAuthorizedException(string message) : base(message) { }
        public UserNotAuthorizedException(string message, Exception inner) : base(message, inner) { }
    }
}
