using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class BatchDTO: Model
    {
        public BatchDTO()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CreatorUserName { get; set; }

        public string Description { get; set; }

        public List<string> Vehicles { get; set; }
    }
}
