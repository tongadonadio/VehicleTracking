using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class HistoricVehicle
    {

        public HistoricVehicle() { }

        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public string Vin { get; set; }

        public string CurrentLocation { get; set; }

        public string Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

    }
}
