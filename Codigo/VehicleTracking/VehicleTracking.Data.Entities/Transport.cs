using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class Transport : Entity
    {
        public Transport()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public User IdUser { get; set; }

        public List<Batch> Batches { get; set; }
    }
}