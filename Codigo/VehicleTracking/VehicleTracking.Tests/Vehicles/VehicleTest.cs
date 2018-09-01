using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests
{

    [TestClass]
    public class VehicleTest
    {

        [TestMethod]
        public void CreateVehicleTest()
        {
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2016, "Gris", "Auto", "TEST1234");
            vehicle.CurrentLocation = "Puerto";
            vehicle.Status = StatusCode.InPort;

            Assert.AreEqual("Chevrolet", vehicle.Brand);
            Assert.AreEqual("Onyx", vehicle.Model);
            Assert.AreEqual(2016, vehicle.Year);
            Assert.AreEqual("Gris", vehicle.Color);
            Assert.AreEqual("Auto", vehicle.Type);
            Assert.AreEqual("TEST1234", vehicle.Vin);
            Assert.AreEqual("Puerto", vehicle.CurrentLocation);
            Assert.AreEqual("En puerto", vehicle.Status);
        }

        [TestMethod]
        public void CreateVehicleReadyToSellTest()
        {
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2017, "Gris", "Auto", "123TEST");
            vehicle.CurrentLocation = "Puerto";
            vehicle.Status = StatusCode.ReadyToSell;

            Assert.AreEqual("Chevrolet", vehicle.Brand);
            Assert.AreEqual("Onyx", vehicle.Model);
            Assert.AreEqual(2017, vehicle.Year);
            Assert.AreEqual("Gris", vehicle.Color);
            Assert.AreEqual("Auto", vehicle.Type);
            Assert.AreEqual("123TEST", vehicle.Vin);
            Assert.AreEqual("Puerto", vehicle.CurrentLocation);
            Assert.AreEqual("Listo para la venta", vehicle.Status);
        }

        [TestMethod]
        public void CreateVehicleSoldTest()
        {
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2017, "Gris", "Auto", "123TEST12");
            vehicle.CurrentLocation = "Puerto";
            vehicle.Status = StatusCode.Sold;

            Assert.AreEqual("Chevrolet", vehicle.Brand);
            Assert.AreEqual("Onyx", vehicle.Model);
            Assert.AreEqual(2017, vehicle.Year);
            Assert.AreEqual("Gris", vehicle.Color);
            Assert.AreEqual("Auto", vehicle.Type);
            Assert.AreEqual("123TEST12", vehicle.Vin);
            Assert.AreEqual("Puerto", vehicle.CurrentLocation);
            Assert.AreEqual("Vendido", vehicle.Status);
        }

    }
}
