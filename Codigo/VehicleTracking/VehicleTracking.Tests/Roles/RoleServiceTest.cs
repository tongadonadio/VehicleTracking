using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using VehicleTracking.Services.Services;
using VehicleTracking.Services.ServiceImplementations;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class RoleServiceTest
    {
        [TestMethod]
        public void ListAllRolesTest()
        {
            RoleService roleService = new RoleServiceImp();

            List<string> roles = roleService.GetRoles();

            Assert.AreEqual(roles.Count, 5);
            Assert.AreEqual("Administrador", roles.FirstOrDefault(s => s.Contains("Administrador")));
            Assert.AreEqual("Operario del Puerto", roles.FirstOrDefault(s => s.Contains("Operario del Puerto")));
            Assert.AreEqual("Transportista", roles.FirstOrDefault(s => s.Contains("Transportista")));
            Assert.AreEqual("Operario del Patio", roles.FirstOrDefault(s => s.Contains("Operario del Patio")));
            Assert.AreEqual("Vendedor", roles.FirstOrDefault(s => s.Contains("Vendedor")));
        }
    }
}
