using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Log;
using VehicleTracking.Log.PlainText;
using VehicleTracking.Log.Events;
using System.Collections.Generic;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.Logs
{
    [TestClass]
    public class LogPlainTextTest
    {
        [TestMethod]
        public void WriteTextLogTest()
        {
            UserDTO user = new UserDTO();
            user.UserName = "UsuarioLog";

            LogEvent logEvent = new LoginEvent(user);

            DateTime date = DateTime.Now;
            logEvent.User = user;
            logEvent.Date = date;
            logEvent.Content = "El usuario " + user.UserName + " se logueó al sistema";

            ILog log = new PlainTextLog("logTest.txt");

            log.WriteEvent(logEvent);
            List<LogEvent> logEvents = log.FindEvents(le => true);

            Assert.IsNotNull(logEvents);
            Assert.IsNotNull(logEvents.Find(le => le.Content == logEvent.Content));
        }

        [TestMethod]
        public void ReadTextLogTest()
        {
            UserDTO user = new UserDTO();
            user.UserName = "UsuarioLog";

            LogEvent logEvent = new LoginEvent(user);

            DateTime date = DateTime.Now;
            logEvent.User = user;
            logEvent.Date = date;
            logEvent.Content = "El usuario " + user.UserName + " se logueó al sistema";

            ILog log = new PlainTextLog("logTest.txt");

            log.WriteEvent(logEvent);
            List<LogEvent> logEvents = log.FindEvents(le => true);

            Assert.IsNotNull(logEvents);
            Assert.IsNotNull(logEvents.Find(le => le.Content == logEvent.Content));
        }
    }
}
