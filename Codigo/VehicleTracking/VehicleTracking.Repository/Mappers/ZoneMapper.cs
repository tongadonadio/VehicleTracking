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
    public class ZoneMapper : Mapper<Zone, ZoneDTO>
    {
        public ZoneDTO ToDTO(Zone zone)
        {
            ZoneDTO zoneDTO = new ZoneDTO();
            zoneDTO.Id = zone.Id;
            zoneDTO.IsSubZone = zone.IsSubZone;
            zoneDTO.MaxCapacity = zone.MaxCapacity;
            zoneDTO.Name = zone.Name;
            zoneDTO.FlowStep = zone.FlowStep != null ? new FlowStepDTO(zone.FlowStep.Name) : null;

            if (zone.Vehicles != null)
            {
                List<VehicleDTO> vehicles = new List<VehicleDTO>();
                VehicleMapper vehicleMapper = new VehicleMapper();
                foreach (Vehicle vehicle in zone.Vehicles)
                {
                    VehicleDTO vehicleDTO = vehicleMapper.ToDTO(vehicle);
                    vehicles.Add(vehicleDTO);
                }
                zoneDTO.Vehicles = vehicles;
            }

            if (zone.SubZones != null)
            {
                List<ZoneDTO> subZonesDTO = new List<ZoneDTO>();
                foreach(Zone subZone in zone.SubZones)
                {
                    ZoneDTO subZoneDTO = new ZoneDTO();
                    subZoneDTO.Id = subZone.Id;
                    subZoneDTO.IsSubZone = subZone.IsSubZone;
                    subZoneDTO.MaxCapacity = subZone.MaxCapacity;
                    subZoneDTO.Name = subZone.Name;
                    subZoneDTO.FlowStep = subZone.FlowStep != null ? new FlowStepDTO(subZone.FlowStep.Name) : null;

                    if (subZone.Vehicles != null)
                    {
                        List<VehicleDTO> vehicles = new List<VehicleDTO>();
                        VehicleMapper vehicleMapper = new VehicleMapper();
                        foreach(Vehicle vehicle in subZone.Vehicles)
                        {
                            VehicleDTO vehicleDTO = vehicleMapper.ToDTO(vehicle);
                            vehicles.Add(vehicleDTO);
                        }
                        subZoneDTO.Vehicles = vehicles;
                    }

                    subZonesDTO.Add(subZoneDTO);
                }
                zoneDTO.SubZones = subZonesDTO;
            }

            return zoneDTO;
        }

        public Zone ToEntity(ZoneDTO zoneDTO)
        {
            Zone zone = new Zone();
            zone.Id = Guid.NewGuid();
            zone.IsSubZone = zoneDTO.IsSubZone;
            zone.MaxCapacity = zoneDTO.MaxCapacity;
            zone.Name = zoneDTO.Name;
            if (zoneDTO.FlowStep != null)
            {
                zone.FlowStep = new FlowStep();
                zone.FlowStep.Name = zoneDTO.FlowStep.Name;
            }

            if (zoneDTO.Vehicles != null)
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                VehicleMapper vehicleMapper = new VehicleMapper();
                foreach (VehicleDTO vehicleDTO in zoneDTO.Vehicles)
                {
                    Vehicle vehicle = vehicleMapper.ToEntity(vehicleDTO);
                    vehicles.Add(vehicle);
                }
                zone.Vehicles = vehicles;
            }

            if (zoneDTO.SubZones != null)
            {
                List<Zone> subZones = new List<Zone>();
                foreach (ZoneDTO subZoneDTO in zoneDTO.SubZones)
                {
                    Zone subZone = new Zone();
                    subZone.Id = Guid.NewGuid();
                    subZone.IsSubZone = subZoneDTO.IsSubZone;
                    subZone.MaxCapacity = subZoneDTO.MaxCapacity;
                    subZone.Name = subZoneDTO.Name;
                    if (subZoneDTO.FlowStep != null)
                    {
                        subZone.FlowStep = new FlowStep();
                        subZone.FlowStep.Name = subZoneDTO.FlowStep.Name;
                    }

                    if (subZoneDTO.Vehicles != null)
                    {
                        List<Vehicle> vehicles = new List<Vehicle>();
                        VehicleMapper vehicleMapper = new VehicleMapper();
                        foreach(VehicleDTO vehicleDTO in subZoneDTO.Vehicles)
                        {
                            Vehicle vehicle = vehicleMapper.ToEntity(vehicleDTO);
                            vehicles.Add(vehicle);
                        }
                        subZone.Vehicles = vehicles;
                    }

                    subZones.Add(subZone);
                }
                zone.SubZones = subZones;
            }

            return zone;
        }
    }
}
