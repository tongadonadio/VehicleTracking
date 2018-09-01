using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using Moq;
using VehicleTracking.Services.Services;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Net;
using VehicleTracking.Web.Api.Controllers;
using System.Web.Http.Results;

namespace VehicleTracking.Tests.Zones
{
    [TestClass]
    public class ZoneControllerTest
    {
        [TestMethod]
        public void CreateZoneTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.MaxCapacity = 60;
            zone.Name = "Zona 1";
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockZoneService = new Mock<ZoneService>();
            mockZoneService.Setup(zs => zs.AddZone(zone)).Verifiable();

            ZoneController zoneController = new ZoneController(mockUserService.Object, mockZoneService.Object, null);
            zoneController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(zoneController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult) zoneController.Create(zone);

            Assert.AreEqual(HttpStatusCode.Created, response.Response.StatusCode);
        }

        [TestMethod]
        public void AssignVehicleTest()
        {
            string vin = "TEST123";
            string zoneId = "644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b";
            Guid zoneGuid = Guid.Parse(zoneId);
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockZoneService = new Mock<ZoneService>();
            mockZoneService.Setup(zs => zs.AssignVehicle(zoneGuid, vin)).Verifiable();

            ZoneController zoneController = new ZoneController(mockUserService.Object, mockZoneService.Object, null);
            zoneController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(zoneController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult) zoneController.AssignVehicle(zoneId, vin);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void AssignZoneTest()
        {
            string zoneId = "533e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b";
            string subZoneId = "644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b";
            Guid zoneGuid = Guid.Parse(zoneId);
            Guid subZoneGuid = Guid.Parse(subZoneId);
            UserDTO user = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(user);
            var mockZoneService = new Mock<ZoneService>();
            mockZoneService.Setup(zs => zs.AssignZone(subZoneGuid, zoneGuid)).Verifiable();

            ZoneController zoneController = new ZoneController(mockUserService.Object, mockZoneService.Object, null);
            zoneController.Request = createUserControllerRequest();
            addTokenHeaderToRequest(zoneController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult) zoneController.AssignZone(subZoneId, zoneId);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }

        private HttpRequestMessage createUserControllerRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return request;
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
    }
}
