using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class RoleDAOTest
    {
        [TestMethod]
        public void GetRoleTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();

            roleDAO.FindRole(Roles.ADMINISTRATOR);
            Role resultRole = roleDAO.FindRole(Roles.ADMINISTRATOR);
            Assert.AreEqual(Roles.ADMINISTRATOR.GetName(), resultRole.Name);

            roleDAO.FindRole(Roles.PORT_OPERATOR);
            resultRole = roleDAO.FindRole(Roles.PORT_OPERATOR);
            Assert.AreEqual(Roles.PORT_OPERATOR.GetName(), resultRole.Name);

            roleDAO.FindRole(Roles.CARRIER);
            resultRole = roleDAO.FindRole(Roles.CARRIER);
            Assert.AreEqual(Roles.CARRIER.GetName(), resultRole.Name);

            roleDAO.FindRole(Roles.PORT_OPERATOR);
            resultRole = roleDAO.FindRole(Roles.PORT_OPERATOR);
            Assert.AreEqual(Roles.PORT_OPERATOR.GetName(), resultRole.Name);

        }
    }
}
