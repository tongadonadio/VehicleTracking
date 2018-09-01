using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface ZoneService
    {

        void AddZone(ZoneDTO zone);

        ZoneDTO FindZoneById(Guid id);

        List<ZoneDTO> GetAllZones();

        void AssignZone(Guid from, Guid to);

        void AssignVehicle(Guid zoneId, string vin);

        void RemoveVehicle(string vin);

        void Update(ZoneDTO zone);

        void Delete(Guid id);

        void AddSubZone(Guid id, ZoneDTO zone);

    }
}
