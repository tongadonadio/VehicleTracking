using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTracking.Repository.Mappers;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using System.Data.Entity;
using VehicleTracking.Data.DataAccess;

namespace VehicleTracking.Tests.Batchs
{
    [TestClass]
    public class BatchMapperTest
    {
        [TestMethod]
        public void MapBatchDTOToBatchTest()
        {
            BatchMapper mapper = new BatchMapper();
            BatchDTO batch = new BatchDTO();
            batch.CreatorUserName = "pepito123";
            batch.Description = "Primer Lote";
            batch.Name = "Lote1";
            batch.Vehicles = new List<string>();
            batch.Vehicles.Add("TEST1234");
            batch.CreatorUserName = "pepito123";

            Batch batchEntity = mapper.ToEntity(batch);

            Assert.AreEqual(batch.Id, batchEntity.Id);
            Assert.AreEqual(batch.CreatorUserName, batchEntity.IdUser.UserName);
            Assert.AreEqual(batch.Description, batchEntity.Description);
            Assert.AreEqual(batch.Name, batchEntity.Name);
            Vehicle vehicle = batchEntity.Vehicles.Find(v => v.Vin == "TEST1234");
            Assert.IsNotNull(vehicle);
        }

        [TestMethod]
        public void MapBatchToBatchDTOTest()
        {
            BatchMapper mapper = new BatchMapper();
            Batch batch = new Batch();
            
            batch.IdUser = this.CreateUser();
            batch.Description = "Primer Lote";
            batch.Name = "Lote1";
            batch.Vehicles = new List<Vehicle>();

            Vehicle vehicle = new Vehicle();
            vehicle.Vin = "TEST1234";
            batch.Vehicles.Add(vehicle);

            BatchDTO batchDTO = mapper.ToDTO(batch);

            Assert.AreEqual(batch.Id, batchDTO.Id);
            Assert.AreEqual(batch.IdUser.UserName, batchDTO.CreatorUserName);
            Assert.AreEqual(batch.Description, batchDTO.Description);
            Assert.AreEqual(batch.Name, batchDTO.Name);
            foreach (Vehicle ve in batch.Vehicles)
            {
                Assert.IsTrue(batchDTO.Vehicles.Contains(ve.Vin));
            }
        }

        private User CreateUser()
        {
            User user = new User();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.IdRole = new Role(Roles.ADMINISTRATOR);

            return user;
        }

    }
}
