using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class UserOrPasswordNotFoundException : Exception
    {

        public UserOrPasswordNotFoundException() : base() { }
        public UserOrPasswordNotFoundException(string message) : base(message) { }
        public UserOrPasswordNotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
