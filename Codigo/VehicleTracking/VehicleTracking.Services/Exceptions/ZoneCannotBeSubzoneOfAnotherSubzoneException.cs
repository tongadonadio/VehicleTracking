using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ZoneCannotBeSubzoneOfAnotherSubzoneException : Exception
    {
        public ZoneCannotBeSubzoneOfAnotherSubzoneException() : base() { }
        public ZoneCannotBeSubzoneOfAnotherSubzoneException(string message) : base(message) { }
        public ZoneCannotBeSubzoneOfAnotherSubzoneException(string message, Exception inner) : base(message, inner) { }
    }
}
