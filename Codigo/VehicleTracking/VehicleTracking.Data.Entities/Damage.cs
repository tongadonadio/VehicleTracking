using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Damage : Entity
    {
        public Damage()
        {
            this.Id = Guid.NewGuid();
        } 

        public Guid Id { get; set; }

        public string Description { get; set; }

        public virtual List<Base64Image> Images { get; set; }
    }
}
