using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Repository;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class HistoryServiceImp : HistoryService
    {

        private HistoryDAO historyDAO;

        public HistoryServiceImp(HistoryDAO historyDAO)
        {
            this.historyDAO = historyDAO;
        }

        public VehicleHistoryDTO GetHistory(string vin)
        {
            return this.historyDAO.GetHistory(vin);
        }
    }
}
