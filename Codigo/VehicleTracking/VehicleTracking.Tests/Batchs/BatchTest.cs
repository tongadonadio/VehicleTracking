using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Linq;

namespace VehicleTracking.Tests
{

    [TestClass]
    public class BatchTest
    {
        [TestMethod]
        public void CreateBatch()
        {
            Batch batch = new Batch();
            User user = new User("Rafael", "Perez", "rperez2017", 099123123, "perez123", new Role());

            batch.Name = "Lote 1";
            batch.IdUser = user;
            batch.Description = "Primer lote de autos";

            List<Vehicle> vehicles = new List<Vehicle>();
            Vehicle vehicle = new Vehicle("Chevrolet", "Onyx", 2016, "Gris", "Auto", "TEST1234");
            vehicles.Add(vehicle);

            batch.Vehicles = vehicles;

            Assert.AreEqual("Lote 1", batch.Name);
            Assert.AreEqual("rperez2017", batch.IdUser.UserName);
            Assert.AreEqual("Primer lote de autos", batch.Description);

            Assert.AreEqual("Chevrolet", vehicles.ElementAt(0).Brand);
            Assert.AreEqual("Onyx", vehicles.ElementAt(0).Model);
            Assert.AreEqual(2016, vehicles.ElementAt(0).Year);
            Assert.AreEqual("Gris", vehicles.ElementAt(0).Color);
            Assert.AreEqual("Auto", vehicles.ElementAt(0).Type);
            Assert.AreEqual("TEST1234", vehicles.ElementAt(0).Vin);
        }
    }
}
