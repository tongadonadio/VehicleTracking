using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Collections.Generic;
using System.IO;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository.Mappers;
using VehicleTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class DamageMapperTest
    {
        [TestMethod]
        public void MapDamageDTOToDamageTest()
        {
            VehicleDTO vehicle = this.CreateVehicle();
            DamageMapper mapper = new DamageMapper();
            Base64ImageDTO base64Image = new Base64ImageDTO();
            base64Image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));

            DamageDTO damage = new DamageDTO();
            damage.Description = "Daño en la puerta derecha";
            damage.Images = new List<Base64ImageDTO>();
            damage.Images.Add(base64Image);
            Damage damageEntity = mapper.ToEntity(damage);

            Assert.AreEqual(damage.Description, damageEntity.Description);
            foreach (Base64ImageDTO image in damage.Images)
            {
                Assert.IsNotNull(damageEntity.Images.Find(i => i.Base64EncodedImage == image.Base64EncodedImage));
            }
        }

        [TestMethod]
        public void MapDamageToDamageDTOTest()
        {
            VehicleDTO vehicle = this.CreateVehicle();
            DamageMapper mapper = new DamageMapper();
            Base64Image base64Image = new Base64Image();
            base64Image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));

            VehicleMapper vehicleMapper = new VehicleMapper();
            Damage damage = new Damage();
            damage.Description = "Daño en la puerta derecha";
            damage.Images = new List<Base64Image>();
            damage.Images.Add(base64Image);
            DamageDTO damageDTO = mapper.ToDTO(damage);

            Assert.AreEqual(damage.Description, damageDTO.Description);
            foreach (Base64Image image in damage.Images)
            {
                Assert.IsNotNull(damageDTO.Images.Find(i => i.Base64EncodedImage == image.Base64EncodedImage));
            }
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
    }
}
