using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Data.DataAccess
{
    public class VehicleTrackingDbContext : DbContext
    {
        static VehicleTrackingDbContext()
        {
            Database.SetInitializer<VehicleTrackingDbContext>(null);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Damage> Damages { get; set; }
        public DbSet<Base64Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<HistoricVehicle> HistoricVehicles { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<FlowStep> FlowSteps { get; set; }
        public DbSet<FlowItem> FlowItems { get; set; }
    }
}
