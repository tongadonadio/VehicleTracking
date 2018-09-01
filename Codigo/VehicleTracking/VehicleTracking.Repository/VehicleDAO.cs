using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface VehicleDAO
    {

        VehicleDTO FindVehicleByVin(string vin);

        void AddVehicle(VehicleDTO vehicle);

        bool IsAssigned(string vin);

        void DeleteVehicleByVin(string vin);

        void UpdateVehicle(VehicleDTO vehicle);

        List<VehicleDTO> GetAllVehicles();

    }
}
