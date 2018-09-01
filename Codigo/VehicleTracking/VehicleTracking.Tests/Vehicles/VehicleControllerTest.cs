using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using VehicleTracking.Services.Services;
using Moq;
using VehicleTracking.Services;
using System.Net;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Web.Api.Controllers;
using System.Web.Http.Results;

namespace VehicleTracking.Tests.Vehicles
{
    [TestClass]
    public class VehicleControllerTest
    {
        [TestMethod]
        public void CreateVehicleSuccessfullyTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.AddVehicle(vehicle)).Verifiable();

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult) vehicleController.Create(vehicle);

            Assert.AreEqual(HttpStatusCode.Created, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateVehicleDuplicatedVinTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.AddVehicle(vehicle)).Throws(new VehicleVinDuplicatedException());

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.Create(vehicle);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateVehicleNullAttributesTest()
        {
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = null; 
            vehicle.Model = null;
            vehicle.Color = null;
            vehicle.Type = null;
            vehicle.Vin = null;
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.AddVehicle(vehicle)).Throws(new VehicleNullAttributesException());

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.Create(vehicle);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void DeleteVehicleTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.DeleteVehicle("TEST1234")).Verifiable();

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.Delete("TEST1234");

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void UpdateVehicleSuccessfullyTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.UpdateVehicle(vehicle)).Verifiable();

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.Update("TEST1234", vehicle);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void UpdateVehicleNotRegisteredTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.UpdateVehicle(vehicle)).Throws(new VehicleNotFoundException());

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.Update("TEST1234", vehicle);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void SetVehicleReadyToSellSuccessfullyTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.SetVehicleReadyToSell(vehicle.Vin)).Verifiable();

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.SetVehicleReadyToSell("TEST1234");

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void SellVehicleSuccessfullyTest()
        {
            VehicleDTO vehicle = CreateVehicle();
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(vs => vs.SellVehicle(vehicle)).Verifiable();

            VehicleController vehicleController = new VehicleController(mockUserService.Object, mockVehicleService.Object, null);
            vehicleController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(vehicleController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)vehicleController.SellVehicle(vehicle);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        private HttpRequestMessage createUserControllerRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return request;
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }

        private UserDTO createUserDTO()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Pepe";
            userDTO.LastName = "Perez";
            userDTO.UserName = "pepito123";
            userDTO.Phone = 091234567;
            userDTO.Password = "123456";
            userDTO.Role = "Administrador";

            return userDTO;
        }

        private VehicleDTO CreateVehicle()
        {
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;

            return vehicle;
        }
    }
}
