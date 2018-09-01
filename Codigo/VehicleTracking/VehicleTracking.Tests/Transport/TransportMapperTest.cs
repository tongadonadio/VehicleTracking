using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Repository.Mappers;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class TransportMapperTest
    {
        [TestMethod]
        public void MapTransportDTOToTransportTest()
        {
            TransportMapper mapper = new TransportMapper();

            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = this.CreateBatchesDTO();
            transport.User = this.CreateUserDTO();

            Transport transportEntity = mapper.ToEntity(transport);

            Assert.AreEqual(transport.Id, transportEntity.Id);
            Assert.AreEqual(transport.StartDate, transportEntity.StartDate);
            Assert.AreEqual(transport.EndDate, transportEntity.EndDate);
            Assert.AreEqual(transport.User.UserName, transportEntity.IdUser.UserName);
            foreach (BatchDTO batch in transport.Batches)
            {
                Assert.IsNotNull(transportEntity.Batches.Find(b => b.Description == batch.Description));
            }
        }

        [TestMethod]
        public void MapTransportToTransportDTOTest()
        {
            TransportMapper mapper = new TransportMapper();

            Transport transport = new Transport();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = this.CreateBatchesEntities();
            transport.IdUser = this.CreateUserEntity();

            TransportDTO transportEntity = mapper.ToDTO(transport);

            Assert.AreEqual(transport.Id, transportEntity.Id);
            Assert.AreEqual(transport.StartDate, transportEntity.StartDate);
            Assert.AreEqual(transport.EndDate, transportEntity.EndDate);
            Assert.AreEqual(transport.IdUser.UserName, transportEntity.User.UserName);
            foreach (Batch batch in transport.Batches)
            {
                Assert.IsNotNull(transportEntity.Batches.Find(b => b.Description == batch.Description));
            }
        }

        private UserDTO CreateUserDTO()
        {
            UserDTO user = new UserDTO();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = "Administrador";

            return user;
        }

        private User CreateUserEntity()
        {
            Role role = new Role();
            role.Name = "Administrador";

            User user = new User();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.IdRole = role;

            return user;
        }

        private List<BatchDTO> CreateBatchesDTO()
        {
            List<BatchDTO> batches = new List<BatchDTO>();
            BatchDTO batch = new BatchDTO();

            batch.Description = "Es el lote 1";
            batch.Description = "pepito123";
            batch.Name = "Lote1";
            batch.CreatorUserName = "pepito123";
            batches.Add(batch);
            List<VehicleDTO> vehiclesDTO = this.CreateVehiclesDTO();
            List<string> vehicles = new List<string>();
            foreach (VehicleDTO vehicle in vehiclesDTO)
            {
                vehicles.Add(vehicle.Vin);
            }
            batch.Vehicles = vehicles;

            return batches;
        }

        private List<Batch> CreateBatchesEntities()
        {
            List<Batch> batches = new List<Batch>();
            Batch batch = new Batch();

            batch.Description = "Es el lote 1";
            batch.Description = "pepito123";
            batch.Name = "Lote1";
            batch.IdUser = this.CreateUserEntity();
            batches.Add(batch);

            List<Vehicle> vehicles = this.CreateVehiclesEntities();
            
            batch.Vehicles = vehicles;

            return batches;
        }

        private List<VehicleDTO> CreateVehiclesDTO()
        {
            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;

            vehicles.Add(vehicle);

            return vehicles;
        }

        private List<Vehicle> CreateVehiclesEntities()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            Vehicle vehicle = new Vehicle();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;

            vehicles.Add(vehicle);

            return vehicles;
        }
    }
}
