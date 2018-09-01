using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository.Mappers;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public class VehicleDAOImpl : VehicleDAO
    {

        private VehicleMapper mapper;

        public VehicleDAOImpl()
        {
            mapper = new VehicleMapper();
        }

        public void AddVehicle(VehicleDTO vehicleDTO)
        {
            Vehicle vehicle = mapper.ToEntity(vehicleDTO);
            vehicle.Id = Guid.NewGuid();
            HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(vehicleDTO);
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                context.Vehicles.Add(vehicle);
                context.HistoricVehicles.Add(historicVehicle);
                context.SaveChanges();
            }
        }

        private HistoricVehicle mapVehicleToHistoricVehicle(VehicleDTO vehicleDTO)
        {
            HistoricVehicle historicVehicle = new HistoricVehicle();

            historicVehicle.Id = Guid.NewGuid();
            historicVehicle.Brand = vehicleDTO.Brand;
            historicVehicle.Color = vehicleDTO.Color;
            historicVehicle.CurrentLocation = vehicleDTO.CurrentLocation;
            historicVehicle.Date = DateTime.Now;
            historicVehicle.Model = vehicleDTO.Model;
            historicVehicle.Status = vehicleDTO.Status;
            historicVehicle.Type = vehicleDTO.Type;
            historicVehicle.Vin = vehicleDTO.Vin;
            historicVehicle.Year = vehicleDTO.Year;

            return historicVehicle;
        }

        public void DeleteVehicleByVin(string vin)
        {
            Vehicle vehicle = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from v in context.Vehicles
                            where v.Vin == vin
                            select v;
                vehicle = query.ToList().FirstOrDefault();

                if(vehicle != null)
                {
                    context.Vehicles.Remove(vehicle);
                    context.SaveChanges();
                }
            }
        }

        public VehicleDTO FindVehicleByVin(string vin)
        {
            Vehicle vehicle = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from v in context.Vehicles
                              where v.Vin == vin
                              select v;
                vehicle = query.ToList().FirstOrDefault();
            }

            VehicleDTO vehicleDTO = null;

            if(vehicle != null)
            {
                vehicleDTO = mapper.ToDTO(vehicle);
            }

            return vehicleDTO;
        }

        public List<VehicleDTO> GetAllVehicles()
        {
            List<VehicleDTO> vehiclesDTO = new List<VehicleDTO>();
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                foreach(Vehicle vehicle in context.Vehicles)
                {
                    VehicleDTO dto = this.mapper.ToDTO(vehicle);
                    vehiclesDTO.Add(dto);
                }
            }
            return vehiclesDTO;
        }

        public bool IsAssigned(string vin)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                VehicleDTO vehicle = FindVehicleByVin(vin);
                bool isAssigned = false;
                if (context.Batches.Count() > 0)
                {
                    isAssigned = context.Batches.Any(b => b.Vehicles.Any(v => v.Vin == vehicle.Vin));
                }
                return isAssigned;
            }
        }

        public void UpdateVehicle(VehicleDTO vehicleDTO)
        {
            Vehicle vehicle = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from v in context.Vehicles
                            where v.Vin == vehicleDTO.Vin
                            select v;
                vehicle = query.ToList().FirstOrDefault();

                if (vehicle != null)
                {
                    vehicle.Model = vehicleDTO.Model;
                    vehicle.Status = vehicleDTO.Status;
                    vehicle.Type = vehicleDTO.Type;
                    vehicle.Vin = vehicleDTO.Vin;
                    vehicle.Year = vehicleDTO.Year;
                    vehicle.Brand = vehicleDTO.Brand;
                    vehicle.Color = vehicleDTO.Color;
                    vehicle.CurrentLocation = vehicleDTO.CurrentLocation;
                    vehicle.Price = vehicleDTO.Price;

                    context.Vehicles.Attach(vehicle);
                    var entry = context.Entry(vehicle);
                    entry.Property(e => e.Model).IsModified = true;
                    entry.Property(e => e.Status).IsModified = true;
                    entry.Property(e => e.Type).IsModified = true;
                    entry.Property(e => e.Vin).IsModified = true;
                    entry.Property(e => e.Year).IsModified = true;
                    entry.Property(e => e.Brand).IsModified = true;
                    entry.Property(e => e.Color).IsModified = true;
                    entry.Property(e => e.CurrentLocation).IsModified = true;
                    entry.Property(e => e.Price).IsModified = true;

                    HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(vehicleDTO);
                    context.HistoricVehicles.Add(historicVehicle);

                    context.SaveChanges();
                }
            }
        }
    }
}
