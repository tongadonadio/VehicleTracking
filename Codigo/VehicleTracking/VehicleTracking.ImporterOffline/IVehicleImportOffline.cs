using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.ImporterOffline
{
    public interface IVehicleImportOffline : IImporterOffline
    {
        List<VehicleDTO> ImportVehicles();
    }
}
