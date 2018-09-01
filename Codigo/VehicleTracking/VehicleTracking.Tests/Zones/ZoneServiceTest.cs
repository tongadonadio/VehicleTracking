using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository;
using Moq;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Exceptions;
using System.Collections.Generic;

namespace VehicleTracking.Tests.Zones
{
    [TestClass]
    public class ZoneServiceTest
    {
        [TestMethod]
        public void CreateZoneSuccessfullyTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.Id = Guid.NewGuid();
            
            var mockZoneDAO = new Mock<ZoneDAO>();
            ZoneDTO mockZone = new ZoneDTO();
            mockZone.Name = "Zona 1";
            mockZone.MaxCapacity = 60;

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(mockZone);

            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();
            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AddZone(zone);
            ZoneDTO resultZone = zoneService.FindZoneById(zone.Id);

            Assert.AreEqual(zone.Name, resultZone.Name);
            Assert.AreEqual(zone.MaxCapacity, resultZone.MaxCapacity);
            Assert.AreEqual(zone.IsSubZone, resultZone.IsSubZone);
        }

        [TestMethod]
        public void CreateSubZoneSuccessfullyTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.IsSubZone = true;
            zone.Id = Guid.NewGuid();
            FlowStepDTO flowStep = new FlowStepDTO("Lavado");
            zone.FlowStep = flowStep;

            var mockZoneDAO = new Mock<ZoneDAO>();
            ZoneDTO mockZone = new ZoneDTO();
            mockZone.Name = "Zona 1";
            mockZone.MaxCapacity = 60;
            mockZone.IsSubZone = true;
            mockZone.FlowStep = new FlowStepDTO("Lavado");
            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(mockZone);

            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();
            mockFlowDAO.Setup(f => f.IsStepAvailable(flowStep)).Returns(false);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AddZone(zone);
            ZoneDTO resultZone = zoneService.FindZoneById(zone.Id);

            Assert.AreEqual(zone.Name, resultZone.Name);
            Assert.AreEqual(zone.MaxCapacity, resultZone.MaxCapacity);
            Assert.AreEqual(zone.IsSubZone, resultZone.IsSubZone);
            Assert.AreEqual(zone.FlowStep.Name, resultZone.FlowStep.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(SubZoneWithoutFlowStepException))]
        public void ErrorCreatingSubZoneWithoutFlowStepTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.IsSubZone = true;
            zone.Id = Guid.NewGuid();

            FlowStepDTO flowStep = new FlowStepDTO("Lavado");
            zone.FlowStep = flowStep;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockFlowDAO.Setup(f => f.IsStepAvailable(flowStep)).Returns(true);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AddZone(zone);
        }

        [TestMethod]
        [ExpectedException(typeof(ZoneNotFoundException))]
        public void FindZoneWrongIdTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();
            Guid newId = Guid.NewGuid();

            mockZoneDAO.Setup(z => z.FindZoneById(newId)).Throws(new ZoneNotFoundException());

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);
            zoneService.FindZoneById(zone.Id); 
        }

        [TestMethod]
        public void AssignZoneSuccessfullyTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.Id = Guid.NewGuid();

            ZoneDTO subZone = new ZoneDTO();
            subZone.Name = "SubZona 1";
            subZone.MaxCapacity = 60;
            subZone.Id = Guid.NewGuid();

            List<ZoneDTO> subZones = new List<ZoneDTO>();
            subZones.Add(subZone);
            zone.SubZones = subZones;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(zone);
            mockZoneDAO.Setup(z => z.FindZoneById(subZone.Id)).Returns(subZone);
            mockZoneDAO.Setup(z => z.GetZoneCapacityLeft(zone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignZone(subZone.Id, zone.Id)).Verifiable();

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignZone(subZone.Id, zone.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ZoneCannotBeSubzoneOfAnotherSubzoneException))]
        public void AssignSubZoneToAnotherSubZoneErrorTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.IsSubZone = true;
            zone.Id = Guid.NewGuid();

            ZoneDTO subZone = new ZoneDTO();
            subZone.Name = "SubZona 1";
            subZone.MaxCapacity = 60;
            subZone.Id = Guid.NewGuid();

            List<ZoneDTO> subZones = new List<ZoneDTO>();
            subZones.Add(subZone);
            zone.SubZones = subZones;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(zone);
            mockZoneDAO.Setup(z => z.FindZoneById(subZone.Id)).Returns(subZone);
            mockZoneDAO.Setup(z => z.GetZoneCapacityLeft(zone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignZone(subZone.Id, zone.Id)).Throws(new ZoneCannotBeSubzoneOfAnotherSubzoneException());

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignZone(subZone.Id, zone.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ZoneOutOfCapacityException))]
        public void AssignSubZoneToAZoneWithoutCapacityTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.Id = Guid.NewGuid();

            ZoneDTO subZone = new ZoneDTO();
            subZone.Name = "SubZona 1";
            subZone.MaxCapacity = 70;
            subZone.Id = Guid.NewGuid();

            List<ZoneDTO> subZones = new List<ZoneDTO>();
            subZones.Add(subZone);
            zone.SubZones = subZones;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(zone);
            mockZoneDAO.Setup(z => z.FindZoneById(subZone.Id)).Returns(subZone);
            mockZoneDAO.Setup(z => z.GetZoneCapacityLeft(zone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignZone(subZone.Id, zone.Id)).Throws(new ZoneOutOfCapacityException());

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignZone(subZone.Id, zone.Id);
        }

        [TestMethod]
        public void AssignVehicleSuccessfullyTest()
        {
            ZoneDTO firstZone = new ZoneDTO();
            firstZone.Name = "SubZona 1";
            firstZone.MaxCapacity = 60;
            firstZone.Id = Guid.NewGuid();
            firstZone.IsSubZone = true;
            FlowStepDTO firstStep = new FlowStepDTO("Mecanica ligera");
            firstZone.FlowStep = firstStep;

            ZoneDTO secondZone = new ZoneDTO();
            secondZone.Name = "SubZona 2";
            secondZone.MaxCapacity = 60;
            secondZone.Id = Guid.NewGuid();
            secondZone.IsSubZone = true;
            FlowStepDTO secondStep = new FlowStepDTO("Lavado");
            secondZone.FlowStep = secondStep;

            List<FlowItemDTO> flow = new List<FlowItemDTO>();
            FlowItemDTO firstItem = new FlowItemDTO();
            firstItem.StepNumber = 1;
            firstItem.FlowStep = firstStep;
            FlowItemDTO secondItem = new FlowItemDTO();
            secondItem.StepNumber = 2;
            secondItem.FlowStep = secondStep;
            flow.Add(firstItem);
            flow.Add(secondItem);

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Status = StatusCode.ReadyToBeLocated;
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            firstZone.Vehicles = vehicles;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(firstZone.Id)).Returns(firstZone);
            mockZoneDAO.Setup(z => z.FindZoneById(secondZone.Id)).Returns(secondZone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(firstZone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(secondZone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignVehicle(firstZone.Id, vehicle.Vin)).Verifiable();
            ZoneDTO mockedZone = null;
            mockZoneDAO.Setup(z => z.GetVehicleZone(vehicle.Vin)).Returns(mockedZone);
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flow);
            mockVehicleDAO.Setup(v => v.FindVehicleByVin(vehicle.Vin)).Returns(vehicle);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(firstZone.Id, vehicle.Vin);

            vehicle.Status = StatusCode.Located;
            mockZoneDAO.Setup(z => z.AssignVehicle(secondZone.Id, vehicle.Vin)).Verifiable();
            mockZoneDAO.Setup(z => z.GetVehicleZone(vehicle.Vin)).Returns(firstZone);

            zoneService.AssignVehicle(secondZone.Id, vehicle.Vin);
        }

        [TestMethod]
        [ExpectedException(typeof(VehicleStatusDoesntAllowToAssignZoneException))]
        public void AssignVehicleErrorVehicleStatusTest()
        {
            ZoneDTO firstZone = new ZoneDTO();
            firstZone.Name = "SubZona 1";
            firstZone.MaxCapacity = 60;
            firstZone.Id = Guid.NewGuid();
            firstZone.IsSubZone = true;
            FlowStepDTO firstStep = new FlowStepDTO("Mecanica ligera");
            firstZone.FlowStep = firstStep;

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Status = StatusCode.ReadyToSell;
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            firstZone.Vehicles = vehicles;

            List<FlowItemDTO> flow = new List<FlowItemDTO>();
            FlowItemDTO firstItem = new FlowItemDTO();
            firstItem.StepNumber = 1;
            firstItem.FlowStep = firstStep;
            flow.Add(firstItem);

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(firstZone.Id)).Returns(firstZone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(firstZone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignVehicle(firstZone.Id, vehicle.Vin)).Verifiable();
            ZoneDTO mockedZone = null;
            mockZoneDAO.Setup(z => z.GetVehicleZone(vehicle.Vin)).Returns(mockedZone);
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flow);
            mockVehicleDAO.Setup(v => v.FindVehicleByVin(vehicle.Vin)).Returns(vehicle);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(firstZone.Id, vehicle.Vin);
        }

        [TestMethod]
        [ExpectedException(typeof(FlowStepOrderException))]
        public void AssignVehicleErrorFlowStepOrderTest()
        {
            FlowStepDTO firstStep = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO secondStep = new FlowStepDTO("Lavado");

            ZoneDTO firstZone = new ZoneDTO();
            firstZone.Name = "SubZona 1";
            firstZone.MaxCapacity = 60;
            firstZone.Id = Guid.NewGuid();
            firstZone.IsSubZone = true;            
            firstZone.FlowStep = secondStep;            

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Status = StatusCode.ReadyToBeLocated;
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            firstZone.Vehicles = vehicles;

            List<FlowItemDTO> flow = new List<FlowItemDTO>();
            FlowItemDTO firstItem = new FlowItemDTO();
            firstItem.StepNumber = 1;
            firstItem.FlowStep = firstStep;
            FlowItemDTO secondItem = new FlowItemDTO();
            secondItem.StepNumber = 2;
            secondItem.FlowStep = secondStep;
            flow.Add(firstItem);
            flow.Add(secondItem);

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(firstZone.Id)).Returns(firstZone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(firstZone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignVehicle(firstZone.Id, vehicle.Vin)).Verifiable();
            ZoneDTO mockedZone = null;
            mockZoneDAO.Setup(z => z.GetVehicleZone(vehicle.Vin)).Returns(mockedZone);
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flow);
            mockVehicleDAO.Setup(v => v.FindVehicleByVin(vehicle.Vin)).Returns(vehicle);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(firstZone.Id, vehicle.Vin);
        }

        [TestMethod]
        [ExpectedException(typeof(SubezoneDoesNotBelongeToZoneException))]
        public void SubzoneDoesNotBelongeToFlowTest()
        {
            FlowStepDTO firstStep = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO secondStep = new FlowStepDTO("Lavado");

            ZoneDTO firstZone = new ZoneDTO();
            firstZone.Name = "SubZona 1";
            firstZone.MaxCapacity = 60;
            firstZone.Id = Guid.NewGuid();
            firstZone.IsSubZone = true;
            firstZone.FlowStep = firstStep;

            ZoneDTO secondZone = new ZoneDTO();
            secondZone.Name = "SubZona 2";
            secondZone.MaxCapacity = 60;
            secondZone.Id = Guid.NewGuid();
            secondZone.IsSubZone = true;
            secondZone.FlowStep = secondStep;

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Status = StatusCode.Located;
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            firstZone.Vehicles = vehicles;

            List<FlowItemDTO> flow = new List<FlowItemDTO>();

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(firstZone.Id)).Returns(firstZone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(firstZone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignVehicle(firstZone.Id, vehicle.Vin)).Verifiable();
            mockZoneDAO.Setup(z => z.GetVehicleZone(vehicle.Vin)).Returns(secondZone);
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flow);
            mockVehicleDAO.Setup(v => v.FindVehicleByVin(vehicle.Vin)).Returns(vehicle);

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(firstZone.Id, vehicle.Vin);
        }

        [TestMethod]
        [ExpectedException(typeof(AssignVehicleToMainZoneException))]
        public void AssignVehicleIsNotSubZoneTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.Id = Guid.NewGuid();
            zone.IsSubZone = false;

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            zone.Vehicles = vehicles;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(zone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(zone.Id)).Returns(60);
            mockZoneDAO.Setup(z => z.AssignVehicle(zone.Id, vehicle.Vin)).Verifiable();
            
            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(zone.Id, vehicle.Vin);
        }

        [TestMethod]
        [ExpectedException(typeof(AssignVehicleToZoneWithoutCapacityException))]
        public void AssignVehicleOutOfCapacityTest()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.MaxCapacity = 60;
            zone.Id = Guid.NewGuid();
            zone.IsSubZone = true;

            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            vehicles.Add(vehicle);
            zone.Vehicles = vehicles;

            var mockZoneDAO = new Mock<ZoneDAO>();
            var mockFlowDAO = new Mock<FlowDAO>();
            var mockVehicleDAO = new Mock<VehicleDAO>();

            mockZoneDAO.Setup(z => z.FindZoneById(zone.Id)).Returns(zone);
            mockZoneDAO.Setup(z => z.GetVehicleCapacityLeft(zone.Id)).Returns(0);
            mockZoneDAO.Setup(z => z.AssignVehicle(zone.Id, vehicle.Vin)).Verifiable();

            var zoneService = new ZoneServiceImp(mockZoneDAO.Object, mockFlowDAO.Object, mockVehicleDAO.Object);

            zoneService.AssignVehicle(zone.Id, vehicle.Vin);
        }

    }
}
