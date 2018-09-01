using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace VehicleTracking.Repository.Mappers
{
    public class VehicleMapper : Mapper<Vehicle, VehicleDTO>
    {
        public VehicleDTO ToDTO(Vehicle vehicle)
        {
            VehicleDTO dto = new VehicleDTO();

            dto.Id = vehicle.Id;
            dto.Model = vehicle.Model;
            dto.Status = vehicle.Status;
            dto.Type = vehicle.Type;
            dto.Vin = vehicle.Vin;
            dto.Year = vehicle.Year;
            dto.Brand = vehicle.Brand;
            dto.Color = vehicle.Color;
            dto.CurrentLocation = vehicle.CurrentLocation;
            dto.Price = vehicle.Price;

            return dto;
        }

        public Vehicle ToEntity(VehicleDTO model)
        {
            Vehicle entity = new Vehicle();

            entity.Id = model.Id;
            entity.Model = model.Model;
            entity.Status = model.Status;
            entity.Type = model.Type;
            entity.Vin = model.Vin;
            entity.Year = model.Year;
            entity.Brand = model.Brand;
            entity.Color = model.Color;
            entity.CurrentLocation = model.CurrentLocation;
            entity.Price = model.Price;

            return entity;
        }
    }
}
