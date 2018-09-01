using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class ImageNotFoundException: Exception
    {
        public ImageNotFoundException() : base() { }
        public ImageNotFoundException(string message) : base(message) { }
        public ImageNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
