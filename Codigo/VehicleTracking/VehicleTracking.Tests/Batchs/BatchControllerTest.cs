using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Services.Services;
using Moq;
using System.Net.Http;
using System.Net;
using VehicleTracking.Web.Api.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using System.Web.Http.Results;

namespace VehicleTracking.Tests.Batchs
{
    [TestClass]
    public class BatchControllerTest
    {
        [TestMethod]
        public void CreateBatchTest()
        {
            BatchDTO batch = createBatchDTO();
            UserDTO userDTO = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.CreateBatch(batch)).Verifiable();
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            BatchController batchController = new BatchController(mockBatchService.Object, mockUserService.Object);
            batchController.Request = createBatchControllerRequest();
            this.addTokenHeaderToRequest(batchController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)batchController.Post(batch);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void ErrorCreateBatchWithoutTokenTest()
        {
            BatchDTO batch = createBatchDTO();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.CreateBatch(batch)).Verifiable();

            BatchController batchController = new BatchController(mockBatchService.Object, null);
            batchController.Request = createBatchControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)batchController.Post(batch);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void ErrorCreateBatchBadTokenTest()
        {
            BatchDTO batch = createBatchDTO();
            UserDTO userDTO = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.CreateBatch(batch)).Verifiable();

            BatchController batchController = new BatchController(mockBatchService.Object, null);
            batchController.Request = createBatchControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)batchController.Post(batch);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void ErrorCreateBatchWhitoutVehiclesTest()
        {
            BatchDTO batch = new BatchDTO();
            batch.CreatorUserName = "Pepe";
            batch.CreatorUserName = "Descripción Lote 1";
            batch.Name = "Lote1";

            UserDTO userDTO = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.CreateBatch(batch)).Throws(new VehicleNotFoundException());
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            BatchController batchController = new BatchController(mockBatchService.Object, mockUserService.Object);
            batchController.Request = createBatchControllerRequest();
            this.addTokenHeaderToRequest(batchController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)batchController.Post(batch);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateListAllBatchesTest()
        {
            List<BatchDTO> batches = new List<BatchDTO>();
            batches.Add(createBatchDTO());

            UserDTO userDTO = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.GetAllBatches()).Returns(batches);
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            BatchController batchController = new BatchController(mockBatchService.Object, mockUserService.Object);
            batchController.Request = createBatchControllerRequest();
            this.addTokenHeaderToRequest(batchController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)batchController.GetAll();

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void GetBatchByIdTest()
        {
            BatchDTO batch = createBatchDTO();

            UserDTO userDTO = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockBatchService = new Mock<BatchService>();
            mockBatchService.Setup(b => b.FindBatchById(batch.Id)).Returns(batch);
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            BatchController batchController = new BatchController(mockBatchService.Object, mockUserService.Object);
            batchController.Request = createBatchControllerRequest();
            this.addTokenHeaderToRequest(batchController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)batchController.Get(batch.Id.ToString());

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        private BatchDTO createBatchDTO()
        {
            BatchDTO batch = new BatchDTO();
            batch.Id = Guid.NewGuid();
            batch.CreatorUserName = "Pepe";
            batch.CreatorUserName = "Descripción Lote 1";
            batch.Name = "Lote1";
            batch.Vehicles = new List<string>();
            batch.Vehicles.Add("Vehiculo1");
            batch.Vehicles.Add("Vehiculo2");
            return batch;
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }

        private HttpRequestMessage createBatchControllerRequest()
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
