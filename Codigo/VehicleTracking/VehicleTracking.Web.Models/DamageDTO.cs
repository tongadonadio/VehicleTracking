using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Web.Models
{
    public class DamageDTO : Model
    {

        public string Description { get; set; }

        public List<Base64ImageDTO> Images { get; set; }

    }
}
