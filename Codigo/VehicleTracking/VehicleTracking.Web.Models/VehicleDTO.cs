using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class VehicleDTO : Model
    {

        public VehicleDTO() { }
        
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public string Vin { get; set; }

        public string CurrentLocation { get; set; }

        public string Status { get; set; }

        public int Price { get; set; }
    }
}
