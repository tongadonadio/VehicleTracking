using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;


namespace VehicleTracking.Tests
{
    [TestClass]
    public class RoleTest
    {
        [TestMethod]
        public void CreateRoleTest()
        {

            Role role = new Role(Roles.ADMINISTRATOR);

            Assert.AreEqual(Roles.ADMINISTRATOR.GetName(), role.Name);
            Assert.AreEqual("Administrador", role.Name);

            role = new Role(Roles.PORT_OPERATOR);

            Assert.AreEqual(Roles.PORT_OPERATOR.GetName(), role.Name);
            Assert.AreEqual("Operario del Puerto", role.Name);

            role = new Role(Roles.CARRIER);

            Assert.AreEqual(Roles.CARRIER.GetName(), role.Name);
            Assert.AreEqual("Transportista", role.Name);

            role = new Role(Roles.YARD_OPERATOR);

            Assert.AreEqual(Roles.YARD_OPERATOR.GetName(), role.Name);
            Assert.AreEqual("Operario del Patio", role.Name);
        }
    }
}
