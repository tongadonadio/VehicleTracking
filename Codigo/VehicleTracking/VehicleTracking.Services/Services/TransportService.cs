using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface TransportService
    {
        Guid CreateTransport(TransportDTO transport);

        TransportDTO FindTransportById(Guid id);

        List<TransportDTO> GetAllTransports();

        void StartTransport(List<Guid> batchesId, UserDTO user);

        void FinishTransport(Guid id);
    }
}
