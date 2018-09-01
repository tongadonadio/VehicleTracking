using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface HistoryService
    {

        VehicleHistoryDTO GetHistory(string vin);

    }
}
