using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Tests
{

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            Role role = new Role(Roles.ADMINISTRATOR);
            User user = new User("Rafael", "Perez", "rperez2017", 099123123, "perez123", role);

            Assert.AreEqual("Rafael", user.Name);
            Assert.AreEqual("Perez", user.LastName);
            Assert.AreEqual("rperez2017", user.UserName);
            Assert.AreEqual(099123123, user.Phone);
            Assert.AreEqual("perez123", user.Password);
            Assert.AreEqual(role, user.IdRole);
        }

        [TestMethod]
        public void CreateSellerUserTest()
        {
            Role role = new Role(Roles.SELLER);
            User user = new User("Mario", "Lopez", "mlopez2017", 099333333, "mlopez123", role);

            Assert.AreEqual("Mario", user.Name);
            Assert.AreEqual("Lopez", user.LastName);
            Assert.AreEqual("mlopez2017", user.UserName);
            Assert.AreEqual(099333333, user.Phone);
            Assert.AreEqual("mlopez123", user.Password);
            Assert.AreEqual(role, user.IdRole);
        }
    }
}
