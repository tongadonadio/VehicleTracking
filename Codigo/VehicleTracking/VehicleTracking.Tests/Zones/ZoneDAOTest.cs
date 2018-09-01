using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Repository;
using System.Linq;

namespace VehicleTracking.Tests.Zones
{
    [TestClass]
    public class ZoneDAOTest
    {
        [TestMethod]
        public void PersistZoneTest()
        {
            ZoneDAO zoneDAO = new ZoneDAOImp();
            ZoneDTO expectedDto = createZone("Zona 1");
            Guid id = zoneDAO.CreateZone(expectedDto);

            ZoneDTO resultZone = zoneDAO.FindZoneById(id);

            Assert.AreEqual(expectedDto.Name, resultZone.Name);
            Assert.AreEqual(expectedDto.IsSubZone, resultZone.IsSubZone);
            Assert.AreEqual(expectedDto.MaxCapacity, resultZone.MaxCapacity);
            Assert.AreEqual(expectedDto.SubZones, resultZone.SubZones);
            Assert.AreEqual(expectedDto.Vehicles, resultZone.Vehicles);
            Assert.IsNull(resultZone.SubZones);
            Assert.IsNull(resultZone.Vehicles);
        }

        [TestMethod]
        public void AssignZoneTest()
        {
            ZoneDAO zoneDAO = new ZoneDAOImp();
            ZoneDTO zoneDTO = createZone("Zona 1");
            ZoneDTO subZoneDTO = createZone("Zona 2");

            Guid idZone = zoneDAO.CreateZone(zoneDTO);
            Guid idSubZone = zoneDAO.CreateZone(subZoneDTO);
            ZoneDTO resultZone = zoneDAO.FindZoneById(idZone);
            ZoneDTO resultSubZone = zoneDAO.FindZoneById(idSubZone);
            zoneDAO.AssignZone(idSubZone, idZone);

            resultZone = zoneDAO.FindZoneById(idZone);

            Assert.AreEqual(resultZone.SubZones.ElementAt(0).Name, resultSubZone.Name);
            Assert.IsTrue(resultZone.SubZones.ElementAt(0).IsSubZone);
            Assert.AreEqual(resultZone.SubZones.ElementAt(0).MaxCapacity, resultSubZone.MaxCapacity);
            Assert.IsNull(resultZone.SubZones.ElementAt(0).SubZones);
        }

        [TestMethod]
        public void AssignVehicleTest()
        {
            ZoneDAO zoneDAO = new ZoneDAOImp();
            ZoneDTO zoneDTO = createZone("Zona 1");
            VehicleDTO expectedVehicle = CreateVehicle("TEST123456");
            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO resultVehicle = vehicleDAO.FindVehicleByVin("TEST123456");
            if (resultVehicle == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            }

            Guid idZone = zoneDAO.CreateZone(zoneDTO);
            zoneDAO.AssignVehicle(idZone, expectedVehicle.Vin);

            ZoneDTO resultZone = zoneDAO.FindZoneById(idZone);

            Assert.AreEqual(resultZone.Vehicles.ElementAt(0).Vin, expectedVehicle.Vin);
        }

        private ZoneDTO createZone(string name)
        {
            ZoneDTO zone = new ZoneDTO();
            zone.Name = name;
            zone.IsSubZone = false;
            zone.MaxCapacity = 60;

            return zone;
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
