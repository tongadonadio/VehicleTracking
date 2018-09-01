using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface ZoneDAO
    {

        Guid CreateZone(ZoneDTO zoneDTO);

        ZoneDTO FindZoneById(Guid id);

        List<ZoneDTO> GetAllZones();

        void AssignZone(Guid From, Guid To);

        void AssignVehicle(Guid idZone, string vin);

        int GetZoneCapacityLeft(Guid zoneId);

        int GetVehicleCapacityLeft(Guid zoneId);

        void RemoveVehicle(string vin);

        ZoneDTO GetVehicleZone(string vin);

        void Update(ZoneDTO zone);

        void Delete(Guid id);

        void CreateSubZone(Guid id, ZoneDTO zone);

    }
}
