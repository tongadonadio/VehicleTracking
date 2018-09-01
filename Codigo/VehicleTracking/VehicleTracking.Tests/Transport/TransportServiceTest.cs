using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using Moq;
using VehicleTracking.Repository;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;
using VehicleTracking.Services;
using VehicleTracking.Data.Entities;
using WebTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class TransportServiceTest
    {
        [TestMethod]
        public void CreateTransportSuccessfullyTest()
        {
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = new List<BatchDTO>();
            transport.User = new UserDTO();

            var mockTransportDAO = new Mock<TransportDAO>();
            mockTransportDAO.Setup(vs => vs.FindTransportById(transport.Id)).Returns(transport);

            var mockInspectionService = new Mock<InspectionService>();
            var mockBatchService = new Mock<BatchService>();
            var mockVehicleService = new Mock<VehicleService>();

            var transportService = new TransportServiceImpl(mockTransportDAO.Object, mockInspectionService.Object, mockBatchService.Object, mockVehicleService.Object);

            Guid id = transportService.CreateTransport(transport);

            TransportDTO resultTransport = transportService.FindTransportById(transport.Id);

            Assert.AreEqual(transport.Batches, resultTransport.Batches);
            Assert.AreEqual(transport.EndDate.Date, resultTransport.EndDate.Date);
            Assert.AreEqual(transport.Id, resultTransport.Id);
            Assert.AreEqual(transport.StartDate.Date, resultTransport.StartDate.Date);
            Assert.AreEqual(transport.User, resultTransport.User);
        }

        [TestMethod]
        public void StartTransportTest()
        {
            VehicleDTO vehicleDTO = new VehicleDTO();
            List<Guid> batches = new List<Guid>();
            List<VehicleDTO> vehicles = this.CreateVehicles();
            UserDTO userDTO = this.CreateUser();

            var mockInspectionService = new Mock<InspectionService>();
            mockInspectionService.Setup(vs => vs.ExistVehicleInspection("123")).Returns(true);
            var mockVehicleService = new Mock<VehicleService>();
            mockVehicleService.Setup(v => v.FindVehicleByVin("TEST1234")).Returns(vehicleDTO);
            mockVehicleService.Setup(v => v.UpdateVehicle(vehicleDTO));
            var mockTransportDAO = new Mock<TransportDAO>();
            var mockBatchService = new Mock<BatchService>();
            
            var transportService = new TransportServiceImpl(mockTransportDAO.Object, mockInspectionService.Object, mockBatchService.Object, mockVehicleService.Object);

            transportService.StartTransport(batches, userDTO);

            foreach (VehicleDTO vehicle in vehicles)
            {
                Assert.AreEqual(vehicle.Status, StatusCode.InTransit);
            }
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

            var mockTransportDAO = new Mock<TransportDAO>();
            mockTransportDAO.Setup(vs => vs.GetAllTransports()).Returns(listTransports);
            var mockInspectionService = new Mock<InspectionService>();
            var mockBatchService = new Mock<BatchService>();
            var mockVehicleService = new Mock<VehicleService>();

            var transportService = new TransportServiceImpl(mockTransportDAO.Object,mockInspectionService.Object,mockBatchService.Object, mockVehicleService.Object);

            List<TransportDTO> resultListTransport = transportService.GetAllTransports();

            Assert.IsNotNull(resultListTransport.Find(t => t.Id == transport.Id));
        }

        private List<BatchDTO> CreateBatches()
        {
            BatchDTO batch = new BatchDTO();
            batch.Description = "Es el lote 1";
            batch.Description = "pepito123";
            batch.Name = "Lote1";
            batch.CreatorUserName = "pepito123";
            List<VehicleDTO> vehiclesDTO = this.CreateVehicles();
            List<string> vehicles = new List<string>();
            foreach (VehicleDTO vehicle in vehiclesDTO)
            {
                vehicles.Add(vehicle.Vin);
            }
            batch.Vehicles = vehicles;

            List<BatchDTO> batches = new List<BatchDTO>();
            batches.Add(batch);

            return batches;
        }

        private List<VehicleDTO> CreateVehicles()
        {
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InTransit;

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);

            return vehicles;
        }

        private UserDTO CreateUser()
        {
            UserDTO user = new UserDTO();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = Roles.ADMINISTRATOR.GetName();

            return user;
        }
    }
}
