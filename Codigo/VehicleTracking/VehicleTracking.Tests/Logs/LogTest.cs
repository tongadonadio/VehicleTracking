using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Log.Events;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.Logs
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void CreateLogLoginTest()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserName = "UsuarioLog";

            LogEvent log = new LoginEvent(userDTO);
            
            DateTime date = DateTime.Now;
            log.User = userDTO;
            log.Date = date;
            log.Content = "El usuario " + userDTO.UserName + " se logueó al sistema";
            
            Assert.AreEqual("Login", log.Type);
            Assert.AreEqual(date, log.Date);
            Assert.AreEqual(userDTO, log.User);
            Assert.AreEqual("El usuario " + userDTO.UserName + " se logueó al sistema", log.Content);
        }

        [TestMethod]
        public void CreateLogVehicleImportTest()
        {
            UserDTO user = new UserDTO();
            user.UserName = "UsuarioLog";
            VehicleDTO vehicle = new VehicleDTO();
            vehicle.Vin = "Test123";

            LogEvent log = new VehicleImportEvent(user,vehicle);

            DateTime date = DateTime.Now;
            log.User = user;
            log.Date = date;
            log.Content = "Se importaron vehículos";

            Assert.AreEqual("Vehicle Import", log.Type);
            Assert.AreEqual(date, log.Date);
            Assert.AreEqual(user, log.User);
            Assert.AreEqual("Se importaron vehículos", log.Content);
        }
    }
}
