using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.IO;
using VehicleTracking.Repository;
using System.Collections.Generic;
using Moq;
using WebTracking.Repository;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Exceptions;

namespace VehicleTracking.Tests.Inspections
{
    [TestClass]
    public class InspectionServiceTest
    {
        [TestMethod]
        public void CreateInspectionSuccessfullyTest()
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

            var mockInspectionDAO = new Mock<InspectionDAO>();
            mockInspectionDAO.Setup(vs => vs.FindInspectionById(inspection.Id)).Returns(inspection);
            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin(inspection.IdVehicle)).Returns(vehicle);

            var inspectionService = new InspectionServiceImpl(mockInspectionDAO.Object, mockVehicleDAO.Object);

            Guid id = inspectionService.CreateInspection(inspection);

            InspectionDTO resultInspection = inspectionService.FindInspectionById(inspection.Id);

            Assert.AreEqual(inspection.CreatorUserName, resultInspection.CreatorUserName);
            Assert.AreEqual(inspection.Date, resultInspection.Date);
            Assert.AreEqual(inspection.Id, resultInspection.Id);
            Assert.AreEqual(inspection.Location, resultInspection.Location);
            Assert.AreEqual(inspection.IdVehicle, resultInspection.IdVehicle);
            foreach (DamageDTO damage in inspection.Damages)
            {
                Assert.IsNotNull(resultInspection.Damages.Find(d => d.Description == damage.Description));
            }
        }

        [TestMethod]
        public void ExistVehicleInspectionTest()
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

            var mockInspectionDAO = new Mock<InspectionDAO>();
            mockInspectionDAO.Setup(vs => vs.ExistVehicleInspection(vehicle.Vin)).Returns(true);
            var mockVehicleDAO = new Mock<VehicleDAO>();

            var inspectionService = new InspectionServiceImpl(mockInspectionDAO.Object, mockVehicleDAO.Object);

            Assert.IsTrue(inspectionService.ExistVehicleInspection(vehicle.Vin));
        }

        [ExpectedException(typeof(InspectionNotFoundException))]
        [TestMethod]
        public void ErrorCreatingInspectionYardWithoutInspectionPortTest()
        {
            List<DamageDTO> damages = this.CreateDamages();
            UserDTO user = this.CreateUser();
            VehicleDTO vehicle = this.CreateVehicle();

            InspectionDTO inspection = new InspectionDTO();
            inspection.Damages = damages;
            inspection.CreatorUserName = user.UserName;
            inspection.Date = DateTime.Now;
            inspection.Location = "Patio 1";
            inspection.IdVehicle = vehicle.Vin;

            var mockInspectionDAO = new Mock<InspectionDAO>();
            mockInspectionDAO.Setup(vs => vs.ExistInspectionInPort(inspection.IdVehicle)).Returns(false);
            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(v => v.FindVehicleByVin(vehicle.Vin)).Returns(vehicle);

            var inspectionService = new InspectionServiceImpl(mockInspectionDAO.Object, mockVehicleDAO.Object);

            Guid id = inspectionService.CreateInspection(inspection);
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
            expectedVehicle.Status = StatusCode.InPort;

            return expectedVehicle;
        }

        private List<DamageDTO> CreateDamages()
        {
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
