using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public sealed class Ports : Locations
    {
        private readonly string name;

        public static readonly Ports FIRST_PORT = new Ports("Puerto 1");
        public static readonly Ports SECOND_PORT = new Ports("Puerto 2");
        public static readonly Ports THIRD_PORT = new Ports("Puerto 3");

        private Ports(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}
