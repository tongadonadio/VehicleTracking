using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using Moq;
using VehicleTracking.Repository;
using VehicleTracking.Services.Handler;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class PermissionHandlerTest
    {
        [TestMethod]
        public void TestPortOperatorPermissions()
        {
            string portOperator = "Operario del Puerto";

            PermissionHandler permissionHandler = new PermissionHandler();

            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateBatch(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateVehicle(portOperator));
            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateInspectionOnPort(portOperator));

            Assert.IsFalse(permissionHandler.IsUserAllowedToTransportBatches(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToFinishTransportation(portOperator));
            Assert.IsTrue(permissionHandler.IsUserAllowedToListBatches(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateInspectionOnYard(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToLocateVehiclesOnZones(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToMoveSubZonesToZones(portOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToListZones(portOperator));
        }

        [TestMethod]
        public void TestCarrierPermissions()
        {
            string carrier = "Transportista";

            PermissionHandler permissionHandler = new PermissionHandler();

            Assert.IsTrue(permissionHandler.IsUserAllowedToTransportBatches(carrier));
            Assert.IsTrue(permissionHandler.IsUserAllowedToFinishTransportation(carrier));
            Assert.IsTrue(permissionHandler.IsUserAllowedToListBatches(carrier));

            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateBatch(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateVehicle(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateInspectionOnPort(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateInspectionOnYard(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToLocateVehiclesOnZones(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToMoveSubZonesToZones(carrier));
            Assert.IsFalse(permissionHandler.IsUserAllowedToListZones(carrier));
        }

        [TestMethod]
        public void TestYardOperatorPermissions()
        {
            string yardOperator = "Operario del Patio";

            PermissionHandler permissionHandler = new PermissionHandler();

            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateInspectionOnYard(yardOperator));
            Assert.IsTrue(permissionHandler.IsUserAllowedToLocateVehiclesOnZones(yardOperator));
            Assert.IsTrue(permissionHandler.IsUserAllowedToMoveSubZonesToZones(yardOperator));
            Assert.IsTrue(permissionHandler.IsUserAllowedToListZones(yardOperator));

            Assert.IsFalse(permissionHandler.IsUserAllowedToTransportBatches(yardOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToFinishTransportation(yardOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToListBatches(yardOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateBatch(yardOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateVehicle(yardOperator));
            Assert.IsFalse(permissionHandler.IsUserAllowedToCreateInspectionOnPort(yardOperator));
        }

        [TestMethod]
        public void TestAdministratorPermissions()
        {
            string admin = "Administrador";

            PermissionHandler permissionHandler = new PermissionHandler();

            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateInspectionOnYard(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToLocateVehiclesOnZones(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToMoveSubZonesToZones(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToListZones(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToTransportBatches(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToFinishTransportation(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToListBatches(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateBatch(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateVehicle(admin));
            Assert.IsTrue(permissionHandler.IsUserAllowedToCreateInspectionOnPort(admin));
        }
    }
}
