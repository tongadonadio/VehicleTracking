using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Linq;
using System.Collections.Generic;
using VehicleTracking.Repository;
using Moq;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.Batchs
{
    [TestClass]
    public class BatchServiceTest
    {
        [TestMethod]
        public void CreateBatchSuccessfullyTest()
        {
            VehicleDAO vehicleDAO = new VehicleDAOImpl();

            List<string> vehicles = new List<string>();
            vehicles.Add("TEST1234");

            if (!vehicleDAO.IsAssigned("TEST1234"))
            {
                Role role = new Role(Roles.PORT_OPERATOR);
                UserDTO user = new UserDTO();
                user.Name = "Carlos";
                user.LastName = "Perez";
                user.UserName = "carlitosp81";
                user.Phone = 091234567;
                user.Password = "PasswordTest";
                user.Role = Roles.PORT_OPERATOR.GetName();
                BatchDTO batch = new BatchDTO();
                batch.Name = "Lote 1";
                batch.Description = "Primer lote";
                batch.Vehicles = vehicles;
                batch.CreatorUserName = user.UserName;

                var mockBatchDAO = new Mock<BatchDAO>();
                mockBatchDAO.Setup(vs => vs.FindBatchById(batch.Id)).Returns(batch);
                var mockUserDAO = new Mock<UserDAO>();
                mockUserDAO.Setup(vs => vs.FindUserByUserName(batch.CreatorUserName)).Returns(user);

                var batchService = new BatchServiceImpl(mockBatchDAO.Object, mockUserDAO.Object, vehicleDAO);

                Guid batchId = batchService.CreateBatch(batch);

                BatchDTO resultBatch = batchService.FindBatchById(batchId);

                Assert.AreEqual(batch.Id, resultBatch.Id);
                Assert.AreEqual(batch.Name, resultBatch.Name);
                Assert.AreEqual(batch.Description, resultBatch.Description);
                Assert.AreEqual(batch.CreatorUserName, resultBatch.CreatorUserName);
                Assert.AreEqual(batch.Vehicles, resultBatch.Vehicles);
            }
        }

        [ExpectedException(typeof(VehicleNotFoundException))]
        [TestMethod]
        public void ErrorCreatingBatchWithoutVehiclesTest()
        {
            Role role = new Role(Roles.PORT_OPERATOR);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "PasswordTest";
            user.Role = Roles.PORT_OPERATOR.GetName();

            List<string> vehicles = new List<string>();

            BatchDTO batch = new BatchDTO();
            batch.Id = Guid.NewGuid();
            batch.Name = "Lote 1";
            batch.Description = "Primer lote";
            batch.Vehicles = vehicles;
            batch.CreatorUserName = user.UserName;

            var mockBatchDAO = new Mock<BatchDAO>();
            mockBatchDAO.Setup(vs => vs.FindBatchById(batch.Id)).Returns(batch);
            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(vs => vs.FindUserByUserName(batch.CreatorUserName)).Returns(user);
            VehicleDAO vehicleDAO = new VehicleDAOImpl();

            var batchService = new BatchServiceImpl(mockBatchDAO.Object, mockUserDAO.Object, vehicleDAO);

            Guid batchId = batchService.CreateBatch(batch);
        }

        [ExpectedException(typeof(VehicleInOtherBatchException))]
        [TestMethod]
        public void ErrorCreatingBatchVehicleAssignedInOtherBatchTest()
        {
            Role role = new Role(Roles.PORT_OPERATOR);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "PasswordTest";
            user.Role = Roles.PORT_OPERATOR.GetName();

            List<string> vehicles = new List<string>();
            vehicles.Add("TEST1234");

            BatchDTO batch = new BatchDTO();
            batch.Id = Guid.NewGuid();
            batch.Name = "Lote 1";
            batch.Description = "Primer lote";
            batch.Vehicles = vehicles;
            batch.CreatorUserName = user.UserName;

            var mockBatchDAO = new Mock<BatchDAO>();
            mockBatchDAO.Setup(vs => vs.FindBatchById(batch.Id)).Returns(batch);
            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(vs => vs.FindUserByUserName(batch.CreatorUserName)).Returns(user);
            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(v => v.IsAssigned("TEST1234")).Returns(true);
            mockVehicleDAO.Setup(v => v.FindVehicleByVin("TEST1234")).Returns(new VehicleDTO());

            var batchService = new BatchServiceImpl(mockBatchDAO.Object, mockUserDAO.Object, mockVehicleDAO.Object);

            Guid batchId1 = batchService.CreateBatch(batch);
        }

        [TestMethod]
        public void CreateBatchSuccessfullyAsAdministratorTest()
        {
            VehicleDAO vehicleDAO = new VehicleDAOImpl();

            List<Vehicle> vehicles = new List<Vehicle>();
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2016, "Gris", "Auto", "TEST1234");
            vehicle.CurrentLocation = "Puerto";
            vehicles.Add(vehicle);

            if (!vehicleDAO.IsAssigned("TEST1234"))
            {
                Role role = new Role(Roles.ADMINISTRATOR);
                UserDTO user = new UserDTO();
                user.Name = "Carlos";
                user.LastName = "Perez";
                user.UserName = "carlitosp81";
                user.Phone = 091234567;
                user.Password = "PasswordTest";
                user.Role = Roles.PORT_OPERATOR.GetName();

                BatchDTO batch = new BatchDTO();
                batch.Id = Guid.NewGuid();
                batch.Name = "Lote 1";
                batch.Description = "Primer lote";
                batch.Vehicles = new List<string>();
                batch.Vehicles.Add(vehicle.Vin);
                batch.CreatorUserName = user.UserName;

                var mockBatchDAO = new Mock<BatchDAO>();
                mockBatchDAO.Setup(vs => vs.FindBatchById(batch.Id)).Returns(batch);
                var mockUserDAO = new Mock<UserDAO>();
                mockUserDAO.Setup(vs => vs.FindUserByUserName(batch.CreatorUserName)).Returns(user);

                var batchService = new BatchServiceImpl(mockBatchDAO.Object, mockUserDAO.Object, vehicleDAO);

                Guid batchId = batchService.CreateBatch(batch);

                BatchDTO resultBatch = batchService.FindBatchById(batchId);

                Assert.AreEqual(batch.Id, resultBatch.Id);
                Assert.AreEqual(batch.Name, resultBatch.Name);
                Assert.AreEqual(batch.Description, resultBatch.Description);
                Assert.AreEqual(batch.CreatorUserName, resultBatch.CreatorUserName);
                Assert.AreEqual(batch.Vehicles, resultBatch.Vehicles);
            }
        }

        private UserDTO CreateUser()
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
            return expectedUser;
        }

        private VehicleDTO CreateVehicle()
        {
            VehicleDTO expectedVehicle = new VehicleDTO();


            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO vehicleExist = vehicleDAO.FindVehicleByVin(expectedVehicle.Vin);
            if (vehicleExist == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            }
            return expectedVehicle;
        }
    }
}
