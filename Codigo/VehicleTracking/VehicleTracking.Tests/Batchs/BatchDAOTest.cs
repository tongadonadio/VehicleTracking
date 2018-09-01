using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Collections.Generic;
using VehicleTracking.Repository;
using WebTracking.Repository;
using VehicleTracking.Web.Models;
using VehicleTracking.Services;

namespace VehicleTracking.Tests.Batchs
{
    [TestClass]
    public class BatchDAOTest
    {
        [TestMethod]
        public void PersistBatchTest()
        {
            this.CreateUser();
            if (!this.CreateVehicle()) {
                List<string> vehicles = new List<string>();
                vehicles.Add("TEST1234");

                BatchDTO expectedBatch = new BatchDTO();
                expectedBatch.Name = "Lote 1";
                expectedBatch.CreatorUserName = "pepito123";
                expectedBatch.Description = "Primer lote de autos";
                expectedBatch.Vehicles = vehicles;

                BatchDAO batchDAO = new BatchDAOImp();
                Guid idBatch = batchDAO.AddBatch(expectedBatch);

                BatchDTO resultBatch = batchDAO.FindBatchById(idBatch);

                Assert.AreEqual(idBatch, resultBatch.Id);
                Assert.AreEqual(expectedBatch.CreatorUserName, resultBatch.CreatorUserName);
                Assert.AreEqual(expectedBatch.Description, resultBatch.Description);
                Assert.AreEqual(expectedBatch.Name, resultBatch.Name);
                foreach (var vehicle in expectedBatch.Vehicles)
                {
                    Assert.IsNotNull(resultBatch.Vehicles.Find(v => v == vehicle));
                }
            }
        }

        private void CreateUser()
        {
            UserDTO expectedUser = new UserDTO();

            expectedUser.Name = "Pepe";
            expectedUser.LastName = "Lopez";
            expectedUser.UserName = "pepito123";
            expectedUser.Password = "PasswordTest";
            expectedUser.Phone = 091234567;
            expectedUser.Role = "Administrador";

            UserDAO userDAO = new UserDAOImp();
            UserDTO userExist = userDAO.FindUserByUserName(expectedUser.UserName);
            if (userExist == null)
            {
                userDAO.AddUser(expectedUser);
            }
        }

        private bool CreateVehicle()
        {
            VehicleDTO expectedVehicle = new VehicleDTO();
            expectedVehicle.Brand = "Chevrolet";
            expectedVehicle.Model = "Onyx";
            expectedVehicle.Year = 2016;
            expectedVehicle.Color = "Gris";
            expectedVehicle.Type = "Auto";
            expectedVehicle.Vin = "TEST1234";
            expectedVehicle.Id = Guid.NewGuid();
            expectedVehicle.Status = StatusCode.InPort;
            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO vehicleExist = vehicleDAO.FindVehicleByVin(expectedVehicle.Vin);
            bool isAssigned = false;
            if (vehicleExist == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            } else
            {
                isAssigned = vehicleDAO.IsAssigned("TEST1234");
            }
            return isAssigned;
        }
    }
}
