using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VehicleTracking.Web.Models;
using Moq;
using VehicleTracking.Services.Services;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Net;
using VehicleTracking.Web.Api.Controllers;
using VehicleTracking.Repository;
using System.IO;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.Exceptions;
using WebTracking.Repository;
using System.Web.Http.Results;

namespace VehicleTracking.Tests.Inspections
{
    [TestClass]
    public class InspectionControllerTest
    {
        [TestMethod]
        public void CreateInspectionTest()
        {
            List<DamageDTO> damages = this.CreateDamages();
            UserDTO user = this.CreateUser();
            VehicleDTO vehicle = this.CreateVehicle();

            InspectionDTO inspection = new InspectionDTO();
            inspection.Damages = damages;
            inspection.CreatorUserName = user.UserName;
            inspection.Date = DateTime.Now;
            inspection.Location = "Puerto 1";
            inspection.IdVehicle = vehicle.Vin;

            UserDTO userDTO = this.CreateUser();
            Guid token = Guid.NewGuid();

            var mockInspectionService = new Mock<InspectionService>();
            mockInspectionService.Setup(b => b.CreateInspection(inspection)).Verifiable();
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userDTO);

            InspectionController inspectionController = new InspectionController(mockInspectionService.Object, mockUserService.Object);
            inspectionController.Request = createInspectionControllerRequest();
            this.addTokenHeaderToRequest(inspectionController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)inspectionController.Post(inspection);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void ErrorCreateInspectionWithoutTokenTest()
        {
            InspectionDTO inspection = new InspectionDTO();
            
            var mockInspectionService = new Mock<InspectionService>();
            mockInspectionService.Setup(b => b.CreateInspection(inspection)).Verifiable();
            
            InspectionController inspectionController = new InspectionController(mockInspectionService.Object, null);
            inspectionController.Request = createInspectionControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)inspectionController.Post(inspection);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void ErrorCreateInspectionBadTokenTest()
        {
            InspectionDTO inspection = new InspectionDTO();
            Guid token = Guid.NewGuid();

            var mockInspectionService = new Mock<InspectionService>();
            mockInspectionService.Setup(b => b.CreateInspection(inspection)).Verifiable();
            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Throws(new UserNotExistException());

            InspectionController inspectionController = new InspectionController(mockInspectionService.Object, mockUserService.Object);
            inspectionController.Request = createInspectionControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)inspectionController.Post(inspection);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }

        private HttpRequestMessage createInspectionControllerRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return request;
        }

        private VehicleDTO CreateVehicle()
        {
            VehicleDTO expectedVehicle = new VehicleDTO();
            expectedVehicle.Brand = "Chevrolet";
            expectedVehicle.Color = "Gris";
            expectedVehicle.Model = "Onyx";
            expectedVehicle.Type = "Auto";
            expectedVehicle.Vin = "TEST1234";
            expectedVehicle.Year = 2016;

            return expectedVehicle;
        }

        private List<DamageDTO> CreateDamages()
        {
            VehicleDTO vehicle = this.CreateVehicle();
            Base64ImageDTO base64Image = new Base64ImageDTO();
            base64Image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));

            DamageDTO damage = new DamageDTO();
            damage.Description = "Daño en la puerta derecha";
            damage.Images = new List<Base64ImageDTO>();
            damage.Images.Add(base64Image);

            List<DamageDTO> damages = new List<DamageDTO>();
            damages.Add(damage);
            return damages;
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
