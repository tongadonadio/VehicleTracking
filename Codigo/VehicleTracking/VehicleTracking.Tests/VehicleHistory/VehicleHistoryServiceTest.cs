﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Repository;
using Moq;
using VehicleTracking.Services.ServiceImplementations;

namespace VehicleTracking.Tests.VehicleHistory
{
    [TestClass]
    public class VehicleHistoryServiceTest
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

            var mockHistoryDAO = new Mock<HistoryDAO>();
            mockHistoryDAO.Setup(h => h.GetHistory("TEST1234")).Returns(history);

            HistoryServiceImp service = new HistoryServiceImp(mockHistoryDAO.Object);
            Assert.IsNotNull(service.GetHistory("TEST1234"));
        }
    }
}
