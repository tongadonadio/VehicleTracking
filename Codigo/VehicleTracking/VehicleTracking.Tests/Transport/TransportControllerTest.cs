using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using Moq;
using VehicleTracking.Services.Services;
using System.Net.Http;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;
using VehicleTracking.Web.Api.Controllers;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Repository;
using System.Web.Http.Results;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class TransportControllerTest
    {
        [TestMethod]
        public void StartTransportTest()
        {
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = new List<BatchDTO>();
            transport.User = new UserDTO();
            List<Guid> listBatches = new List<Guid>();
            listBatches.Add(new Guid());

            UserDTO userDTO = this.CreateUser();
            Guid token = Guid.NewGuid();

            var mockTransportService = new Mock<TransportService>();
            mockTransportService.Setup(vs => vs.FindTransportById(transport.Id)).Returns(transport);
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            TransportController transportController = new TransportController(mockTransportService.Object, mockUserService.Object);
            transportController.Request = createTransportControllerRequest();
            this.addTokenHeaderToRequest(transportController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)transportController.Start(listBatches);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void FinishTransportTest()
        {
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.Batches = new List<BatchDTO>();
            transport.User = new UserDTO();

            UserDTO userDTO = this.CreateUser();
            Guid token = Guid.NewGuid();

            var mockTransportService = new Mock<TransportService>();
            mockTransportService.Setup(vs => vs.CreateTransport(transport)).Returns(transport.Id);
            mockTransportService.Setup(vs => vs.FinishTransport(transport.Id));
            mockTransportService.Setup(vs => vs.FindTransportById(transport.Id)).Returns(transport);
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            TransportController transportController = new TransportController(mockTransportService.Object, mockUserService.Object);
            transportController.Request = createTransportControllerRequest();
            this.addTokenHeaderToRequest(transportController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)transportController.Finish(transport.Id);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void GetAllTransportTest()
        {
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = new List<BatchDTO>();
            transport.User = new UserDTO();
            List<TransportDTO> listTransports = new List<TransportDTO>();
            listTransports.Add(transport);

            UserDTO userDTO = this.CreateUser();
            Guid token = Guid.NewGuid();

            var mockTransportService = new Mock<TransportService>();
            mockTransportService.Setup(vs => vs.GetAllTransports()).Returns(listTransports);
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            TransportController transportController = new TransportController(mockTransportService.Object, mockUserService.Object);
            transportController.Request = createTransportControllerRequest();
            this.addTokenHeaderToRequest(transportController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)transportController.GetAll();

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }

        private HttpRequestMessage createTransportControllerRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return request;
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

            return expectedUser;
        }

    }
}
