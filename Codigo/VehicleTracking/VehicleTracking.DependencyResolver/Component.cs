﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.DependencyResolver
{
    public interface Component
    {
        void SetUp(RegisterComponent registerComponent);
    }
}
