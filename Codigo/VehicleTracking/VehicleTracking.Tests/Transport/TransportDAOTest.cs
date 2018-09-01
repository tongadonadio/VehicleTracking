using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Repository;
using VehicleTracking.Data.Entities;
using WebTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class TransportDAOTest
    {
        [TestMethod]
        public void PersistTransportTest()
        {
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = this.CreateBatches();
            transport.User = this.CreateUser();

            TransportDAO transportDAO = new TransportDAOImp();
            transportDAO.AddTransport(transport);
            TransportDTO resultTransport = transportDAO.FindTransportById(transport.Id);

            Assert.AreEqual(transport.EndDate.Date, resultTransport.EndDate.Date);
            Assert.AreEqual(transport.Id, resultTransport.Id);
            Assert.AreEqual(transport.StartDate.Date, resultTransport.StartDate.Date);
            Assert.AreEqual(transport.User.UserName, resultTransport.User.UserName);
            foreach (BatchDTO batch in transport.Batches)
            {
                Assert.IsNotNull(resultTransport.Batches.Find(b => b.Description == batch.Description));
            }
        }

        [TestMethod]
        public void UpdateTransportTest()
        {
            DateTime date1 = DateTime.Now;
            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = date1;
            transport.Batches = this.CreateBatches();
            transport.User = this.CreateUser();

            TransportDAO transportDAO = new TransportDAOImp();
            transportDAO.AddTransport(transport);

            DateTime date2 = DateTime.Now;
            transport.EndDate = date2;

            transportDAO.UpdateTransport(transport);

            TransportDTO resultTransport = transportDAO.FindTransportById(transport.Id);

            Assert.AreNotEqual(date1, resultTransport.EndDate.Date);
            Assert.AreNotEqual(date2, resultTransport.EndDate.Date);
            Assert.AreEqual(transport.Id, resultTransport.Id);
            Assert.AreEqual(transport.StartDate.Date, resultTransport.StartDate.Date);
            Assert.AreEqual(transport.User.UserName, resultTransport.User.UserName);
            foreach (BatchDTO batch in transport.Batches)
            {
                Assert.IsNotNull(resultTransport.Batches.Find(b => b.Description == batch.Description));
            }
        }

        [TestMethod]
        public void GetAllTransportTest()
        {
            TransportDTO transportDTO = new TransportDTO();

            TransportDTO transport = new TransportDTO();
            transport.StartDate = DateTime.Now;
            transport.EndDate = DateTime.Now;
            transport.Batches = this.CreateBatches();
            transport.User = this.CreateUser();

            TransportDAO transportDAO = new TransportDAOImp();
            transportDAO.AddTransport(transport);
            List<TransportDTO> transports = transportDAO.GetAllTransports();

            Assert.IsNotNull(transports.Find(t => t.Id == transport.Id));
        }

        private UserDTO CreateUser()
        {
            UserDTO user = new UserDTO();
            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = Roles.ADMINISTRATOR.GetName();

            UserDAO userDAO = new UserDAOImp();
            userDAO.AddUser(user);

            return user;
        }

        private List<BatchDTO> CreateBatches()
        {
            List<BatchDTO> batches = new List<BatchDTO>();
            BatchDTO batch = new BatchDTO();

            batch.Description = "Es el lote 1";
            batch.Description = "pepito123";
            batch.Name = "Lote1";
            batch.CreatorUserName = "pepito123";
            List<VehicleDTO> vehiclesDTO = this.CreateVehicles();
            List<string> vehicles = new List<string>();
            foreach (VehicleDTO vehicle in vehiclesDTO)
            {
                vehicles.Add(vehicle.Vin);
            }
            batch.Vehicles = vehicles;

            BatchDAO batchDAO = new BatchDAOImp();
            batchDAO.AddBatch(batch);

            batches.Add(batch);

            return batches;
        }

        private List<VehicleDTO> CreateVehicles()
        {
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

            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            vehicleDAO.AddVehicle(vehicle);

            vehicles.Add(vehicle);

            return vehicles;
        }
    }
}
