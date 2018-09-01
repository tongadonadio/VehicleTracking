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
    public class ZoneDAOImp : ZoneDAO
    {

        private ZoneMapper mapper;

        public ZoneDAOImp()
        {
            mapper = new ZoneMapper();
        }

        public void AssignZone(Guid From, Guid To)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Zone subZone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == From)
                    .ToList().FirstOrDefault();

                Zone zone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == To)
                    .ToList().FirstOrDefault();

                List<Zone> zones = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .ToList();

                Zone exParentZone = zones.Find(z => z.SubZones.Contains(subZone));

                if (!zone.IsSubZone)
                {
                    int capacityLeft = GetZoneCapacityLeft(To);
                    
                    if(subZone.MaxCapacity <= capacityLeft)
                    {
                        context.Zones.Attach(subZone);
                        zone.SubZones.Add(subZone);

                        if (exParentZone != null)
                        {
                            exParentZone.SubZones.Remove(subZone);
                        }

                        subZone.IsSubZone = true;                        
                        var entrySZ = context.Entry(subZone);
                        entrySZ.Property(sz => sz.IsSubZone).IsModified = true;                        

                        context.SaveChanges();
                    }
                }
            }
        }

        public int GetZoneCapacityLeft(Guid id)
        {
            int capacity = 0;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Zone zone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == id)
                    .ToList().FirstOrDefault();

                capacity = zone.MaxCapacity;
                foreach(Zone subZone in zone.SubZones)
                {
                    capacity = capacity - subZone.MaxCapacity;
                }
            }

            return capacity;
        }

        public Guid CreateZone(ZoneDTO zoneDTO)
        {
            Guid id;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Zone zone = this.mapper.ToEntity(zoneDTO);
                id = zone.Id;
                if(zoneDTO.FlowStep != null) { 
                    var query = from f in context.FlowSteps
                                where f.Name == zoneDTO.FlowStep.Name
                                select f;
                    FlowStep flowStep = query.ToList().FirstOrDefault();
                    zone.FlowStep = flowStep;
                    context.FlowSteps.Attach(flowStep);
                }
                context.Zones.Add(zone);
                context.SaveChanges();
            }
            return id;
        }

        public ZoneDTO FindZoneById(Guid id)
        {
            Zone zone = null;
            ZoneDTO zoneDTO = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                zone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == id)
                    .ToList().FirstOrDefault();
            
                if(zone != null)
                {
                    if (zone.SubZones.Count == 0)
                    {
                        zone.SubZones = null;
                    }
                    if (zone.Vehicles.Count == 0)
                    {
                        zone.Vehicles = null;
                    }
                    zoneDTO = this.mapper.ToDTO(zone);
                }
            }
            return zoneDTO;
        }

        public List<ZoneDTO> GetAllZones()
        {
            List<Zone> zonesEntities = new List<Zone>();
            List<ZoneDTO> zonesDTO = new List<ZoneDTO>();
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                zonesEntities = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Include("FlowStep")
                    .ToList();
                foreach (Zone zone in zonesEntities)
                {
                    ZoneDTO zoneDTO = mapper.ToDTO(zone);
                    zonesDTO.Add(zoneDTO);                    
                }
            }
            return zonesDTO;
        }

        public void AssignVehicle(Guid idZone, string vin)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Vehicle vehicle = context.Vehicles
                    .Where(z => z.Vin == vin)
                    .ToList().FirstOrDefault();

                Zone zone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == idZone)
                    .ToList().FirstOrDefault();

                int capacityLeft = GetVehicleCapacityLeft(idZone);

                if (capacityLeft > 0)
                {
                    context.Vehicles.Attach(vehicle);
                    if(zone.Vehicles == null)
                    {
                        zone.Vehicles = new List<Vehicle>();
                    }

                    zone.Vehicles.Add(vehicle);
                    vehicle.Status = StatusCode.Located;
                    var entry = context.Entry(vehicle);
                    entry.Property(sz => sz.Status).IsModified = true;

                    HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(vehicle);
                    context.HistoricVehicles.Add(historicVehicle);

                    context.SaveChanges();
                }
            }
        }

        private HistoricVehicle mapVehicleToHistoricVehicle(Vehicle vehicle)
        {
            HistoricVehicle historicVehicle = new HistoricVehicle();

            historicVehicle.Id = Guid.NewGuid();
            historicVehicle.Brand = vehicle.Brand;
            historicVehicle.Color = vehicle.Color;
            historicVehicle.CurrentLocation = vehicle.CurrentLocation;
            historicVehicle.Date = DateTime.Now;
            historicVehicle.Model = vehicle.Model;
            historicVehicle.Status = vehicle.Status;
            historicVehicle.Type = vehicle.Type;
            historicVehicle.Vin = vehicle.Vin;
            historicVehicle.Year = vehicle.Year;

            return historicVehicle;
        }

        public int GetVehicleCapacityLeft(Guid zoneId)
        {
            int capacity = 0;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Zone zone = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .Where(z => z.Id == zoneId)
                    .ToList().FirstOrDefault();

                capacity = zone.MaxCapacity;
                
                if(zone.Vehicles != null)
                {
                    capacity = capacity - zone.Vehicles.Count;
                }
            }

            return capacity;
        }

        public void RemoveVehicle(string vin)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Vehicle vehicle = context.Vehicles
                    .Where(v => v.Vin == vin)
                    .ToList().FirstOrDefault();

                Zone zone = context.Zones
                    .Where(z => z.Vehicles.Select(v => v.Id).Contains(vehicle.Id))
                    .ToList().FirstOrDefault();
                zone.Vehicles.Remove(vehicle);
                vehicle.Status = StatusCode.ReadyToBeLocated;
                context.SaveChanges();
            }
        }

        private List<Vehicle> GetVehicles(VehicleTrackingDbContext context, List<Vehicle> vehicles)
        {
            List<Vehicle> resultVehicles = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                Vehicle ve = context.Vehicles
                .Where(v => v.Vin == vehicle.Vin)
                .ToList().FirstOrDefault();
                resultVehicles.Add(ve);
            }
            return resultVehicles;
        }

        public ZoneDTO GetVehicleZone(string vin)
        {
            ZoneDTO zoneDTO = null;
            List<Zone> zones = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                zones = context.Zones
                    .Include("Vehicles")
                    .Include("SubZones")
                    .ToList();

                Zone zone = zones.Find(z => z.Vehicles.Find(v => v.Vin == vin) != null);

                if (zone != null)
                {
                    zoneDTO = this.mapper.ToDTO(zone);
                }
            }
            return zoneDTO;
        }

        public void Update(ZoneDTO zoneDTO)
        {
            Zone zone = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from z in context.Zones
                            where z.Id == zoneDTO.Id
                            select z;
                zone = query.ToList().FirstOrDefault();

                if (zone != null)
                {
                    zone.MaxCapacity = zoneDTO.MaxCapacity;
                    zone.Name = zoneDTO.Name;

                    context.Zones.Attach(zone);
                    var entry = context.Entry(zone);
                    entry.Property(e => e.MaxCapacity).IsModified = true;
                    entry.Property(e => e.Name).IsModified = true;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(Guid id)
        {
            Zone zone = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from z in context.Zones
                            where z.Id == id
                            select z;
                zone = query.ToList().FirstOrDefault();

                if (zone != null)
                {
                    context.Zones.Remove(zone);
                    context.SaveChanges();
                }
            }
        }

        public void CreateSubZone(Guid id, ZoneDTO zoneDTO)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from z in context.Zones
                            where z.Id == id
                            select z;
                Zone mainZone = query.ToList().FirstOrDefault();

                Zone subzone = this.mapper.ToEntity(zoneDTO);
                if (zoneDTO.FlowStep != null)
                {
                    var querySZ = from f in context.FlowSteps
                                where f.Name == zoneDTO.FlowStep.Name
                                select f;
                    FlowStep flowStep = querySZ.ToList().FirstOrDefault();
                    subzone.FlowStep = flowStep;
                    context.FlowSteps.Attach(flowStep);
                }

                context.Zones.Add(subzone);
                if (mainZone != null)
                {
                    mainZone.SubZones.Add(subzone);
                    context.SaveChanges();
                }
            }
        }
    }
}
