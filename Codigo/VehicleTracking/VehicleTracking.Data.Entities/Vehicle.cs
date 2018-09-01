using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Vehicle : Entity
    {
        public Vehicle()
        {
        }

        public Vehicle(string Brand, string Model, int Year, string Color, string Type, string Vin)
        {
            this.Id = Guid.NewGuid();
            this.Brand = Brand;
            this.Model = Model;
            this.Year = Year;
            this.Color = Color;
            this.Type = Type;
            this.Vin = Vin;
        }

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
