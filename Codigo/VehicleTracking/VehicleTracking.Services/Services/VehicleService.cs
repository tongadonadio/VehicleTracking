using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services
{
    public interface VehicleService
    {

        bool IsVinAvailable(string vin);

        void AddVehicle(VehicleDTO vehicle);

        bool IsAssigned(string vin);

        void DeleteVehicle(string vin);

        void UpdateVehicle(VehicleDTO vehicle);

        VehicleDTO FindVehicleByVin(string vin);

        List<VehicleDTO> GetAllVehicles();

        void SetVehicleReadyToSell(string vin);

        void SellVehicle(VehicleDTO vehicle);

    }
}
