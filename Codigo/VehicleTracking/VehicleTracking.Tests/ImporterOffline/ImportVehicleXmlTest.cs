using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.ImporterOffline.VehicleImport.Xml;
using System.Collections.Generic;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.ImporterOffline
{
    [TestClass]
    public class ImportVehicleXmlTest
    {
        [TestMethod]
        public void ImportVehicleFromXmlTest()
        {
            var importer = new VehicleImportOffline();

            List<VehicleDTO> vehicles = importer.ImportVehiclesFromFile(@".\VehiclesXml.xml");

            Assert.AreEqual(1, vehicles.Count);
        }
    }
}
