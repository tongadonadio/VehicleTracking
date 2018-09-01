using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class UserDuplicatedException: Exception
    {
        public UserDuplicatedException() : base() { }
        public UserDuplicatedException(string message) : base(message) { }
        public UserDuplicatedException(string message, Exception inner) : base(message, inner) { }
    }
}
