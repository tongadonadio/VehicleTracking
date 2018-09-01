using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace WebTracking.Repository
{
    public class BatchDAOImp : BatchDAO
    {
        private BatchMapper batchMapper;

        public BatchDAOImp()
        {
            batchMapper = new BatchMapper();
        }

        public Guid AddBatch(BatchDTO batchDTO)
        {
            Batch batch = this.batchMapper.ToEntity(batchDTO);
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var queryUser = from u in context.Users
                            where u.UserName == batchDTO.CreatorUserName
                            select u;
                User user = queryUser.ToList().FirstOrDefault();
                context.Users.Attach(user);

                List<Vehicle> realVehicles = new List<Vehicle>();
                foreach (Vehicle vehicle in batch.Vehicles)
                {
                    var queryVehicle = from v in context.Vehicles
                                where v.Vin == vehicle.Vin
                                select v;
                    Vehicle realVehicle = queryVehicle.ToList().FirstOrDefault();

                    context.Vehicles.Attach(realVehicle);
                    realVehicles.Add(realVehicle);
                    if (realVehicle.Status == StatusCode.InspectedInPort)
                    {
                        realVehicle.Status = StatusCode.ReadyToGo;
                        var entry = context.Entry(realVehicle);
                        entry.Property(sz => sz.Status).IsModified = true;

                        HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(realVehicle);
                        context.HistoricVehicles.Add(historicVehicle);
                    }
                }
                batch.Vehicles = realVehicles;
                batch.IdUser = user;

                if (batch.Vehicles.Count > 0)
                {
                    context.Batches.Add(batch);
                    context.SaveChanges();
                }
            }
            return batch.Id;
        }

        private Inspection GetInspectionOfVehicle(VehicleTrackingDbContext context, Vehicle vehicle)
        {
            Inspection inspection = context.Inspections
                    .Include("IdVehicle")
                    .Where(i => i.IdVehicle.Vin == vehicle.Vin)
                    .ToList().FirstOrDefault();
            return inspection;
        }

        private HistoricVehicle mapVehicleToHistoricVehicle(Vehicle vehicle)
        {
            HistoricVehicle historicVehicle = new HistoricVehicle();

            historicVehicle.Id = Guid.NewGuid();
            historicVehicle.Brand = vehicle.Brand;
            historicVehicle.Color = vehicle.Color;
            historicVehicle.CurrentLocation = vehicle.CurrentLocation;
            historicVehicle.Date = DateTime.Now;
            historicVehicle.Model = vehicle.Model;
            historicVehicle.Status = vehicle.Status;
            historicVehicle.Type = vehicle.Type;
            historicVehicle.Vin = vehicle.Vin;
            historicVehicle.Year = vehicle.Year;

            return historicVehicle;
        }

        public List<BatchDTO> GetAllBatches()
        {
            List<BatchDTO> batches = new List<BatchDTO>();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                List<Batch> batchesEntities = context.Batches
                    .Include("Vehicles")
                    .Include("IdUser")
                    .ToList();
                foreach (Batch batch in context.Batches)
                {
                    BatchDTO batchDTO = this.batchMapper.ToDTO(batch);
                    batches.Add(batchDTO);
                }
            }
            return batches;
        }

        public BatchDTO FindBatchById(Guid id)
        {
            Batch batch = null;
            BatchDTO batchDTO = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                batch = context.Batches
                    .Include("IdUser")
                    .Include("Vehicles")
                    .Where(b => b.Id == id)
                    .ToList().FirstOrDefault();
            }
            if (batch != null)
            {
                batchDTO = this.batchMapper.ToDTO(batch);
            }
            
            return batchDTO;
        }
    }
}
