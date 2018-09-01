using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.Vehicles
{
    [TestClass]
    public class VehicleDAOTest
    {
        [TestMethod]
        public void PersistVehicleTest()
        {
            VehicleDTO expectedVehicle = CreateVehicle("TEST1");

            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            vehicleDAO.AddVehicle(expectedVehicle);
            VehicleDTO resultVehicle = vehicleDAO.FindVehicleByVin("TEST1");

            Assert.AreEqual(expectedVehicle.Brand, resultVehicle.Brand);
            Assert.AreEqual(expectedVehicle.Model, resultVehicle.Model);
            Assert.AreEqual(expectedVehicle.Year, resultVehicle.Year);
            Assert.AreEqual(expectedVehicle.Color, resultVehicle.Color);
            Assert.AreEqual(expectedVehicle.Type, resultVehicle.Type);
            Assert.AreEqual(expectedVehicle.Vin, resultVehicle.Vin);
            Assert.AreEqual(expectedVehicle.Status, resultVehicle.Status);
        }

        [TestMethod]
        public void DeleteVehicleTest()
        {
            VehicleDTO expectedVehicle = CreateVehicle("TEST123");
            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO resultVehicle = vehicleDAO.FindVehicleByVin("TEST123");
            if(resultVehicle == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            }
            vehicleDAO.DeleteVehicleByVin("TEST123");

            resultVehicle = vehicleDAO.FindVehicleByVin("TEST123");

            Assert.IsNull(resultVehicle);
        }

        [TestMethod]
        public void UpdateVehicleTest()
        {
            VehicleDTO expectedVehicle = CreateVehicle("TEST12");
            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO resultVehicle = vehicleDAO.FindVehicleByVin("TEST12");
            if (resultVehicle == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            }

            expectedVehicle.Color = "Azul";

            vehicleDAO.UpdateVehicle(expectedVehicle);

            resultVehicle = vehicleDAO.FindVehicleByVin("TEST12");

            Assert.AreEqual(expectedVehicle.Color, resultVehicle.Color);

            vehicleDAO.DeleteVehicleByVin("TEST12");
        }

        private VehicleDTO CreateVehicle(string vin)
        {
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = vin;
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;

            return vehicle;
        }
    }
}
