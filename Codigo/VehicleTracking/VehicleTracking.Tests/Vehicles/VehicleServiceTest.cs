using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Repository;
using Moq;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Web.Models;
using System.Collections.Generic;

namespace VehicleTracking.Tests.Vehicles
{
    [TestClass]
    public class VehicleServiceTest
    {

        [TestMethod]
        [ExpectedException(typeof(VehicleVinDuplicatedException))]
        public void ErrorCreatingVehicleWithSameVinTest()
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

            var mockVehicleDAO = new Mock<VehicleDAO>();
            VehicleDTO mockVehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("TEST1234")).Returns(mockVehicle);

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, null, null);

            vehicleService.AddVehicle(vehicle);
        }

        [TestMethod]
        public void CreateVehicleSuccessfullyTest()
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

            var mockVehicleDAO = new Mock<VehicleDAO>();
            VehicleDTO mockVehicle = null;
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("TEST1234")).Returns(mockVehicle);

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, null, null);

            vehicleService.AddVehicle(vehicle);
        }

        [TestMethod]
        public void UpdateVehicleSuccessfullyTest()
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

            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("TEST1234")).Returns(vehicle);
            mockVehicleDAO.Setup(vs => vs.UpdateVehicle(vehicle)).Verifiable();

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, null, null);

            vehicleService.UpdateVehicle(vehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(VehicleNotFoundException))]
        public void UpdateVehicleErrorTest()
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
            
            var mockVehicleDAO = new Mock<VehicleDAO>();
            VehicleDTO mockVehicle = null;
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("TEST1234")).Returns(mockVehicle);

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, null, null);

            vehicleService.UpdateVehicle(vehicle);
        }

        [TestMethod]
        public void SetVehicleReadyToBeSoldSuccessfullyTest()
        {
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "SDE1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.Located;

            List<FlowItemDTO> flow = new List<FlowItemDTO>();
            FlowItemDTO flowItem = new FlowItemDTO();
            flowItem.FlowStep = new FlowStepDTO("Lavado");
            flowItem.StepNumber = 1;
            flow.Add(flowItem);

            ZoneDTO zone = new ZoneDTO();
            zone.IsSubZone = true;
            zone.MaxCapacity = 20;
            zone.Vehicles = new List<VehicleDTO>();
            zone.Vehicles.Add(vehicle);
            zone.Name = "SZ prueba";
            zone.FlowStep = new FlowStepDTO("Lavado");

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flow);

            var mockZoneDAO = new Mock<ZoneDAO>();
            mockZoneDAO.Setup(z => z.GetVehicleZone("SDE1234")).Returns(zone);
            mockZoneDAO.Setup(z => z.RemoveVehicle("SDE1234")).Verifiable();

            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("SDE1234")).Returns(vehicle);
            mockVehicleDAO.Setup(vs => vs.UpdateVehicle(vehicle)).Verifiable();

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, mockFlowDAO.Object, mockZoneDAO.Object);

            vehicleService.SetVehicleReadyToSell("SDE1234");
        }

        [TestMethod]
        public void SellVehicleSuccessfullyTest()
        {
            VehicleDTO vehicleToSell = new VehicleDTO();
            vehicleToSell.Vin = "SDE1234";
            vehicleToSell.Price = 100;

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "SDE1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.ReadyToSell;

            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockVehicleDAO.Setup(vs => vs.FindVehicleByVin("SDE1234")).Returns(vehicle);
            mockVehicleDAO.Setup(vs => vs.UpdateVehicle(vehicle)).Verifiable();

            var vehicleService = new VehicleServiceImpl(mockVehicleDAO.Object, null, null);

            vehicleService.SellVehicle(vehicleToSell);
        }

    }
}
