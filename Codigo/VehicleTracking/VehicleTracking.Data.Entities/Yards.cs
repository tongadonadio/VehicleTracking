using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public sealed class Yards : Locations
    {
        private readonly string name;

        public static readonly Yards FIRST_YARD = new Yards("Patio 1");
        public static readonly Yards SECOND_YARD = new Yards("Patio 2");
        public static readonly Yards THIRD_YARD = new Yards("Patio 3");

        private Yards(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}
