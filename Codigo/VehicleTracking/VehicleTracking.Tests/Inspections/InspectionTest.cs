using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using System.Linq;

namespace VehicleTracking.Tests.Inspections
{
    [TestClass]
    public class InspectionTest
    {

        [TestMethod]
        public void CreateInspectionPortTest()
        {
            Inspection inspection = new Inspection();
            
            List<Damage> damages = new List<Damage>();

            Damage damage = new Damage();
            damage.Description = "Puerta rota";

            List<Base64Image> images = new List<Base64Image>();
            Base64Image image = new Base64Image();
            image.Base64EncodedImage = "test";
            images.Add(image);

            damage.Images = images;
            damages.Add(damage);

            inspection.Damages = damages;
            DateTime date = new DateTime();
            inspection.Date = date;
            Location location = new Location(Ports.FIRST_PORT);
            inspection.IdLocation = location;
            inspection.IdUser = new User();
            inspection.IdUser.UserName = "pepito123";
            
            Assert.AreEqual("Puerta rota", inspection.Damages.ElementAt(0).Description);
            Assert.AreEqual("test", inspection.Damages.ElementAt(0).Images.ElementAt(0).Base64EncodedImage);
            Assert.AreEqual(date, inspection.Date);
            Assert.AreEqual(location, inspection.IdLocation);
            Assert.AreEqual("pepito123", inspection.IdUser.UserName);
        }

        public void CreateInspectionYardTest()
        {
            Inspection inspection = new Inspection();
            
            List<Damage> damages = new List<Damage>();

            Damage damage = new Damage();
            damage.Description = "Puerta rota";

            List<Base64Image> images = new List<Base64Image>();
            Base64Image image = new Base64Image();
            image.Base64EncodedImage = "test";
            images.Add(image);

            damage.Images = images;
            damages.Add(damage);

            inspection.Damages = damages;
            DateTime date = new DateTime();
            inspection.Date = date;
            Location location = new Location(Yards.FIRST_YARD);
            inspection.IdLocation = location;
            inspection.IdUser.UserName = "pepito123";
            
            Assert.AreEqual("Puerta rota", inspection.Damages.ElementAt(0).Description);
            Assert.AreEqual("test", inspection.Damages.ElementAt(0).Images.ElementAt(0).Base64EncodedImage);
            Assert.AreEqual(date, inspection.Date);
            Assert.AreEqual(location, inspection.IdLocation);
            Assert.AreEqual("pepito123", inspection.IdUser.UserName);
        }
    }
}
