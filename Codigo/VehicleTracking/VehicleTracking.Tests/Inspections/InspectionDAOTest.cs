using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using WebTracking.Repository;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository;
using System.IO;

namespace VehicleTracking.Tests.Inspections
{
    [TestClass]
    public class InspectionDAOTest
    {
        [TestMethod]
        public void PersistInspectionTest()
        {
            InspectionDAO inspectionDAO = new InspectionDAOImp(new VehicleDAOImpl());
            UserDAO userDAO = new UserDAOImp();

            List<DamageDTO> damages = this.CreateDamages();
            UserDTO user = this.CreateUser();
            VehicleDTO vehicle = this.CreateVehicle();

            InspectionDTO inspection = new InspectionDTO();
            inspection.Damages = damages;
            inspection.CreatorUserName = user.UserName;
            inspection.Date = DateTime.Now;
            inspection.Location = "Puerto 1";
            inspection.IdVehicle = vehicle.Vin;

            Guid id = inspectionDAO.AddInspection(inspection);

            InspectionDTO resultInspection = inspectionDAO.FindInspectionById(inspection.Id);

            Assert.AreEqual(inspection.CreatorUserName, resultInspection.CreatorUserName);
            Assert.AreEqual(inspection.Id, resultInspection.Id);
            Assert.AreEqual(inspection.Location, resultInspection.Location);
            foreach (DamageDTO damage in inspection.Damages)
            {
                Assert.IsNotNull(resultInspection.Damages.Find(d => d.Description == damage.Description));
            }
        }

        [TestMethod]
        public void ExistVehicleInspectionTest()
        {
            InspectionDAO inspectionDAO = new InspectionDAOImp(new VehicleDAOImpl());
            UserDAO userDAO = new UserDAOImp();

            List<DamageDTO> damages = this.CreateDamages();
            UserDTO user = this.CreateUser();
            VehicleDTO vehicle = this.CreateVehicle();

            InspectionDTO inspection = new InspectionDTO();
            inspection.Damages = damages;
            inspection.CreatorUserName = user.UserName;
            inspection.Date = DateTime.Now;
            inspection.Location = "Puerto 1";
            inspection.IdVehicle = vehicle.Vin;

            Guid id = inspectionDAO.AddInspection(inspection);

           Assert.IsTrue( inspectionDAO.ExistVehicleInspection(vehicle.Vin));
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

            VehicleDAO vehicleDAO = new VehicleDAOImpl();
            VehicleDTO vehicleExist = vehicleDAO.FindVehicleByVin(expectedVehicle.Vin);
            if (vehicleExist == null)
            {
                vehicleDAO.AddVehicle(expectedVehicle);
            }
            return (vehicleExist == null) ? expectedVehicle : vehicleExist;
        }

        private List<DamageDTO> CreateDamages()
        {
            Base64ImageDTO base64Image = new Base64ImageDTO();
            base64Image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));

            DamageDTO damage = new DamageDTO();
            damage.Description = "Daño en la puerta derecha";
            damage.Images = new List<Base64ImageDTO>();
            damage.Images.Add(base64Image);

            List<DamageDTO> damages = new List<DamageDTO>();
            damages.Add(damage);
            return damages;
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

            UserDAO userDAO = new UserDAOImp();
            UserDTO userExist = userDAO.FindUserByUserName(expectedUser.UserName);
            if (userExist == null)
            {
                userDAO.AddUser(expectedUser);
            }
            return expectedUser;
        }
    }
}
