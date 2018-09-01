using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public sealed class Roles
    {
        private readonly string name;

        public static readonly Roles ADMINISTRATOR = new Roles("Administrador");
        public static readonly Roles PORT_OPERATOR = new Roles("Operario del Puerto");
        public static readonly Roles CARRIER = new Roles("Transportista");
        public static readonly Roles YARD_OPERATOR = new Roles("Operario del Patio");
        public static readonly Roles SELLER = new Roles("Vendedor");

        private Roles(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        } 
    }
}
