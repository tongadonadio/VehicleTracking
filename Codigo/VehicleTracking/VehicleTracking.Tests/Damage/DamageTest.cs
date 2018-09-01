using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class DamageTest
    {
        [TestMethod]
        public void CreateDamageTest()
        {
            Vehicle vehicle = new Vehicle();
            Damage damage = new Damage();
            damage.Description = "Daño en la puerta derecha";
            Base64Image base64Image = new Base64Image();
            base64Image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));
            damage.Images = new List<Base64Image>();
            damage.Images.Add(base64Image);

            Assert.AreEqual(damage.Description, "Daño en la puerta derecha");
            Assert.IsTrue(damage.Images.Contains(base64Image));
        }
    }
}
