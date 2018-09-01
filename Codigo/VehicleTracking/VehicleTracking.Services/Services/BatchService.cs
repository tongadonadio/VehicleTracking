using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface BatchService
    {
        Guid CreateBatch(BatchDTO batch);

        BatchDTO FindBatchById(Guid id);

        List<BatchDTO> GetAllBatches();
    }
}
