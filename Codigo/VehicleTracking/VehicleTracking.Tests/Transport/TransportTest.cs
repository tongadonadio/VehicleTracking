using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using System.Collections.Generic;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class TransportTest
    {
        [TestMethod]
        public void CreateTransportTest()
        {
            User user = this.CreateUser();
            List<Batch> batches = this.CreateBatches();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            Transport transport = new Transport();
            transport.StartDate = startDate;
            transport.EndDate = endDate;
            transport.IdUser = user;
            transport.Batches = batches;

            Assert.AreEqual(transport.Batches, batches);
            Assert.AreEqual(transport.IdUser, user);
            Assert.AreEqual(transport.EndDate, endDate);
            Assert.AreEqual(transport.StartDate, startDate);
        }

        private User CreateUser()
        {
            Role role = new Role();
            role.Name = "Administrador";

            User user = new User();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.IdRole = role;

            return user;
        }

        private List<Batch> CreateBatches()
        {
            List<Batch> batches = new List<Batch>();
            Batch batch = new Batch();

            batch.Description = "Es el lote 1";
            batch.IdUser = this.CreateUser();
            batch.Name = "Lote1";
            batch.Vehicles = this.CreateVehicles();

            return batches;
        }

        private List<Vehicle> CreateVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            Vehicle vehicle = new Vehicle();
            vehicle.Brand = "Chevrolet";
            vehicle.Model = "Onyx";
            vehicle.Year = 2016;
            vehicle.Color = "Gris";
            vehicle.Type = "Auto";
            vehicle.Vin = "TEST1234";
            vehicle.Id = Guid.NewGuid();
            vehicle.Status = StatusCode.InPort;

            return vehicles;
        }
    }
}
