using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Exceptions;

namespace WebTracking.Repository.Mappers
{
    public class BatchMapper : Mapper<Batch, BatchDTO>
    {
        public BatchMapper()
        {    
        }

        public BatchDTO ToDTO(Batch batch)
        {
            BatchDTO batchDTO = new BatchDTO();

            batchDTO.Id = batch.Id;
            batchDTO.CreatorUserName = batch.IdUser.UserName;
            batchDTO.Description = batch.Description;
            batchDTO.Name = batch.Name;
            batchDTO.Vehicles = new List<string>();
            foreach (var ve in batch.Vehicles)
            {
                batchDTO.Vehicles.Add(ve.Vin);
            }

            return batchDTO;
        }

        public Batch ToEntity(BatchDTO batchDTO)
        {
            UserDAO userDAO = new UserDAOImp();

            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (var vin in batchDTO.Vehicles)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Vin = vin;
                vehicles.Add(vehicle);
            }

            Batch batch = new Batch();
            batch.Id = batchDTO.Id;

            User user = new User();
            user.UserName = batchDTO.CreatorUserName;
            batch.IdUser = user;
            batch.Description = batchDTO.Description;
            batch.Name = batchDTO.Name;
            batch.Vehicles = vehicles;

            return batch;
        }

    }
}
