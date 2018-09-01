using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface BatchDAO
    {
        Guid AddBatch(BatchDTO batchDTO);

        BatchDTO FindBatchById(Guid Id);

        List<BatchDTO> GetAllBatches();
    }
}
