using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using System.Collections.Generic;
using VehicleTracking.Data.Entities;
using WebTracking.Repository.Mappers;
using VehicleTracking.Repository;
using VehicleTracking.Repository.Mappers;

namespace VehicleTracking.Tests.Inspections
{
    [TestClass]
    public class InspectionMapperTest
    {
        [TestMethod]
        public void MapInspectionDTOToInspectionTest()
        {
            UserDTO user = this.CreateUser();
            VehicleDTO vehicle = this.CreateVehicle();

            InspectionMapper mapper = new InspectionMapper();
            InspectionDTO inspectionDTO = new InspectionDTO();
            inspectionDTO.Damages = new List<DamageDTO>();
            inspectionDTO.CreatorUserName = user.UserName;
            inspectionDTO.Date = DateTime.Now;
            inspectionDTO.Location = "Puerto 1";
            inspectionDTO.IdVehicle = vehicle.Vin;

            Inspection inspectionEntity = mapper.ToEntity(inspectionDTO);
            
            Assert.AreEqual(inspectionDTO.Location, inspectionEntity.IdLocation.Name);
            Assert.AreEqual(inspectionDTO.Id, inspectionEntity.Id);
            Assert.AreEqual(inspectionDTO.Date, inspectionEntity.Date);
            Assert.AreEqual(inspectionDTO.CreatorUserName, inspectionEntity.IdUser.UserName);
            Assert.AreEqual(inspectionDTO.IdVehicle, inspectionEntity.IdVehicle.Vin);
            foreach (DamageDTO damageDTO in inspectionDTO.Damages)
            {
                Assert.IsNotNull(inspectionEntity.Damages.Find(d => d.Description == damageDTO.Description));
            }
        }

        [TestMethod]
        public void MapInspectionToInspectionDTOTest()
        {
            UserDTO userDTO = this.CreateUser();
            UserMapper userMapper = new UserMapper(new RoleDAOImp());
            User user = userMapper.ToEntity(userDTO);

            VehicleDTO vehicleDTO = new VehicleDTO();
            VehicleMapper vehicleMapper = new VehicleMapper();
            Vehicle vehicle = vehicleMapper.ToEntity(vehicleDTO);

            InspectionMapper mapper = new InspectionMapper();
            Inspection inspection = new Inspection();
            inspection.Date = DateTime.Now;
            inspection.IdLocation = new Location(Ports.FIRST_PORT);
            inspection.IdUser = user;
            inspection.Damages = new List<Damage>();
            inspection.IdVehicle = vehicle;

            InspectionDTO inspectionDTO = mapper.ToDTO(inspection);
            
            Assert.AreEqual(inspectionDTO.Location, inspection.IdLocation.Name);
            Assert.AreEqual(inspectionDTO.Id, inspection.Id);
            Assert.AreEqual(inspectionDTO.Date, inspection.Date);
            Assert.AreEqual(inspectionDTO.CreatorUserName, inspection.IdUser.UserName);
            Assert.AreEqual(inspectionDTO.IdVehicle, inspection.IdVehicle.Vin);
            foreach (DamageDTO damageDTO in inspectionDTO.Damages)
            {
                Assert.IsNotNull(inspection.Damages.Find(d => d.Description == damageDTO.Description));
            }
        }

        private UserDTO CreateUser()
        {
            UserDTO expectedUser = new UserDTO();

            expectedUser.Name = "Pepe";
            expectedUser.LastName = "Lopez";
            expectedUser.UserName = "pepito123";
            expectedUser.Password = "PasswordTest";
            expectedUser.Phone = 091234567;
            expectedUser.Role = "Administrador";

            return expectedUser;
        }

        private VehicleDTO CreateVehicle()
        {
            VehicleDTO expectedVehicle = new VehicleDTO();
            expectedVehicle.Brand = "Chevrolet";
            expectedVehicle.Color = "Gris";
            expectedVehicle.Model = "Onyx";
            expectedVehicle.Type = "Auto";
            expectedVehicle.Vin = "TEST1234";
            expectedVehicle.Year = 2016;

            return expectedVehicle;
        }
    }
}
