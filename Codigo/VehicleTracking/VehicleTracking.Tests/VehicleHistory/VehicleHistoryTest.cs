using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace VehicleTracking.Tests.VehicleHistory
{
    [TestClass]
    public class VehicleHistoryTest
    {
        [TestMethod]
        public void CreateVehicleHistory()
        {
            VehicleHistoryDTO history = new VehicleHistoryDTO();
            List<HistoricVehicleDTO> vehicles = new List<HistoricVehicleDTO>();

            HistoricVehicleDTO vehicle = new HistoricVehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Status = StatusCode.InPort;
            vehicle.CurrentLocation = "Puerto 1";

            vehicles.Add(vehicle);

            List<InspectionDTO> inspections = new List<InspectionDTO>();
            InspectionDTO inspection = new InspectionDTO();
            inspection.CreatorUserName = "pepe123";
            inspection.Date = DateTime.Now;
            inspection.Location = "Puerto 1";
            inspection.IdVehicle = vehicle.Vin;
            inspections.Add(inspection);

            history.VehicleHistory = vehicles;
            history.InspectionHistory = inspections;

            Assert.AreEqual("Chevrolet", history.VehicleHistory.ElementAt(0).Brand);
            Assert.AreEqual("Onyx", history.VehicleHistory.ElementAt(0).Model);
            Assert.AreEqual(2016, history.VehicleHistory.ElementAt(0).Year);
            Assert.AreEqual("Gris", history.VehicleHistory.ElementAt(0).Color);
            Assert.AreEqual("Auto", history.VehicleHistory.ElementAt(0).Type);
            Assert.AreEqual("TEST1234", history.VehicleHistory.ElementAt(0).Vin);
            Assert.AreEqual("Puerto 1", history.VehicleHistory.ElementAt(0).CurrentLocation);
            Assert.AreEqual("En puerto", history.VehicleHistory.ElementAt(0).Status);

            Assert.AreEqual("pepe123", history.InspectionHistory.ElementAt(0).CreatorUserName);
            Assert.AreEqual(inspection.Date, history.InspectionHistory.ElementAt(0).Date);
            Assert.AreEqual("Puerto 1", history.InspectionHistory.ElementAt(0).Location);
            Assert.AreEqual("TEST1234", history.InspectionHistory.ElementAt(0).IdVehicle);
        }
    }
}
