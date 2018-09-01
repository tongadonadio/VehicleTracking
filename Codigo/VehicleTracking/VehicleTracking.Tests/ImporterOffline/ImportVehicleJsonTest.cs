using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VehicleTracking.Web.Models;
using VehicleTracking.ImporterOffline.VehicleImport.Json;

namespace VehicleTracking.Tests.ImporterOffline
{
    [TestClass]
    public class ImportVehicleJsonTest
    {
        [TestMethod]
        public void ImportVehicleFromJsonTest()
        {
            var importer = new VehicleImportOffline();

            List<VehicleDTO> vehicles = importer.ImportVehiclesFromFile(@".\VehiclesJson.json");

            Assert.AreEqual(1, vehicles.Count);
        }
    }
}
