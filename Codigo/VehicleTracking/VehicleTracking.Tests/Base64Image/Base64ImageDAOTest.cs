using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class Base64ImageDAOTest
    {
        [TestMethod]
        public void PersistBase64ImageTest()
        {
            Base64ImageDAO imageDAO = new Base64ImageDAOImp();

            Base64Image image = new Base64Image();
            image.Base64EncodedImage = Convert.ToBase64String(File.ReadAllBytes(@"..\..\Damage\attention.png"));

            Base64Image existImage = imageDAO.FindImageByBase64Encode(image.Base64EncodedImage);
            if (existImage == null)
            {
                imageDAO.AddImage(image);
            } else
            {
                image.Id = existImage.Id;
            }
            

            Base64Image resultImage = imageDAO.FindImageByBase64Encode(image.Base64EncodedImage);
            
            Assert.AreEqual(image.Id, resultImage.Id);
            Assert.AreEqual(image.Base64EncodedImage, resultImage.Base64EncodedImage);
        }
    }
}
