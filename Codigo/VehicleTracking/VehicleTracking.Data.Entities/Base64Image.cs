using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Base64Image : Entity
    {
        public Base64Image()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Base64EncodedImage { get; set; }
    }
}
