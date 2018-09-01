using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface TransportDAO
    {
        Guid AddTransport(TransportDTO transportDTO);

        TransportDTO FindTransportById(Guid Id);

        List<TransportDTO> GetAllTransports();

        void UpdateTransport(TransportDTO transport);
    }
}
