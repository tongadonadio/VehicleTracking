using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Services.Log;
using VehicleTracking.Log;

namespace VehicleTracking.Tests.Logs
{
    [TestClass]
    public class LogServiceTest
    {
        [TestMethod]
        public void GetInstanceLogTest()
        {
            ILog log = VehicleTrackingLog.GetInstance();
            Assert.IsNotNull(log);

            ILog newLog = VehicleTrackingLog.GetInstance();
            Assert.AreEqual(log, newLog);
        }
    }
}
