using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Collections.Generic;
using VehicleTracking.Web.Models;
using System.Linq;

namespace VehicleTracking.Tests.Zones
{
    [TestClass]
    public class ZoneTest
    {
        [TestMethod]
        public void CreateZone()
        {
            Zone zone = new Zone();
            zone.Name = "Zona 1";
            zone.IsSubZone = false;
            zone.MaxCapacity = 60;

            List<Zone> subZones = new List<Zone>();
            Zone subZone = new Zone();
            subZone.Name = "Subzona 1";
            subZone.IsSubZone = true;
            subZone.MaxCapacity = 30;
            FlowStep flowStep = new FlowStep();
            flowStep.Id = Guid.NewGuid();
            flowStep.Name = "Lavado";
            subZone.FlowStep = flowStep;
            subZones.Add(subZone);
            zone.SubZones = subZones;

            List<Vehicle> vehicles = new List<Vehicle>();
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2016, "Gris", "Auto", "TEST1234");
            vehicle.CurrentLocation = "Puerto";
            vehicle.Status = StatusCode.InPort;
            vehicles.Add(vehicle);

            zone.Vehicles = vehicles;

            Assert.AreEqual("Zona 1", zone.Name);
            Assert.IsFalse(zone.IsSubZone);
            Assert.AreEqual(60, zone.MaxCapacity);
            Assert.IsNotNull(zone.SubZones);
            Assert.AreEqual("Subzona 1", zone.SubZones.ElementAt(0).Name);
            Assert.IsTrue(zone.SubZones.ElementAt(0).IsSubZone);
            Assert.AreEqual(30, zone.SubZones.ElementAt(0).MaxCapacity);
            Assert.AreEqual("Lavado", zone.SubZones.ElementAt(0).FlowStep.Name);

            Assert.AreEqual("Chevrolet", zone.Vehicles.ElementAt(0).Brand);
            Assert.AreEqual("Onyx", zone.Vehicles.ElementAt(0).Model);
            Assert.AreEqual(2016, zone.Vehicles.ElementAt(0).Year);
            Assert.AreEqual("Gris", zone.Vehicles.ElementAt(0).Color);
            Assert.AreEqual("Auto", zone.Vehicles.ElementAt(0).Type);
            Assert.AreEqual("TEST1234", zone.Vehicles.ElementAt(0).Vin);
            Assert.AreEqual("Puerto", zone.Vehicles.ElementAt(0).CurrentLocation);
            Assert.AreEqual("En puerto", zone.Vehicles.ElementAt(0).Status);
        }

        [TestMethod]
        public void CreateZoneDTO()
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = "Zona 1";
            zone.IsSubZone = false;
            zone.MaxCapacity = 60;

            List<ZoneDTO> subZones = new List<ZoneDTO>();
            ZoneDTO subZone = new ZoneDTO();
            subZone.Name = "Subzona 1";
            subZone.IsSubZone = true;
            subZone.MaxCapacity = 30;
            subZone.FlowStep = new FlowStepDTO("Lavado");
            subZones.Add(subZone);
            zone.SubZones = subZones;

            List<VehicleDTO> vehicles = new List<VehicleDTO>();
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;
            vehicle.CurrentLocation = "Puerto";
            vehicles.Add(vehicle);

            zone.Vehicles = vehicles;

            Assert.AreEqual("Zona 1", zone.Name);
            Assert.IsFalse(zone.IsSubZone);
            Assert.AreEqual(60, zone.MaxCapacity);
            Assert.IsNotNull(zone.SubZones);
            Assert.AreEqual("Subzona 1", zone.SubZones.ElementAt(0).Name);
            Assert.IsTrue(zone.SubZones.ElementAt(0).IsSubZone);
            Assert.AreEqual(30, zone.SubZones.ElementAt(0).MaxCapacity);
            Assert.AreEqual("Lavado", zone.SubZones.ElementAt(0).FlowStep.Name);

            Assert.AreEqual("Chevrolet", zone.Vehicles.ElementAt(0).Brand);
            Assert.AreEqual("Onyx", zone.Vehicles.ElementAt(0).Model);
            Assert.AreEqual(2016, zone.Vehicles.ElementAt(0).Year);
            Assert.AreEqual("Gris", zone.Vehicles.ElementAt(0).Color);
            Assert.AreEqual("Auto", zone.Vehicles.ElementAt(0).Type);
            Assert.AreEqual("TEST1234", zone.Vehicles.ElementAt(0).Vin);
            Assert.AreEqual("Puerto", zone.Vehicles.ElementAt(0).CurrentLocation);
            Assert.AreEqual("En puerto", zone.Vehicles.ElementAt(0).Status);
        }
    }
}
