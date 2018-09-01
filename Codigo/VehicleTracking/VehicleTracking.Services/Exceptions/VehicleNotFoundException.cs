﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Services.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() : base() { }
        public VehicleNotFoundException(string message) : base(message) { }
        public VehicleNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
