using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class TransportDTO : Model
    {
        public TransportDTO()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id;

        public DateTime StartDate;

        public DateTime EndDate;

        public List<BatchDTO> Batches;

        public UserDTO User;
    }
}
