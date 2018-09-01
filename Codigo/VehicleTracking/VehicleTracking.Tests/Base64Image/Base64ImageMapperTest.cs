using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository.Mappers;
using VehicleTracking.Repository;

namespace VehicleTracking.Tests
{
    [TestClass]
    public class Base64ImageMapperTest
    {
        [TestMethod]
        public void MapBase64ImageDTOToBase64ImageTest()
        {
            Base64ImageDAO imageDAO = new Base64ImageDAOImp();
            Base64ImageMapper mapper = new Base64ImageMapper(imageDAO);
            Base64ImageDTO image = new Base64ImageDTO();
            image.Base64EncodedImage = "1234";

            Base64Image imageEntity = mapper.ToEntity(image);

            Assert.AreEqual(image.Base64EncodedImage, imageEntity.Base64EncodedImage);
            Assert.IsNotNull(imageEntity.Id);
        }

        [TestMethod]
        public void MapBase64ImageToBase64ImageDTOTest()
        {
            Base64ImageDAO imageDAO = new Base64ImageDAOImp();
            Base64ImageMapper mapper = new Base64ImageMapper(imageDAO);
            Base64Image image = new Base64Image();
            image.Base64EncodedImage = "1234";

            Base64ImageDTO imageDTO = mapper.ToDTO(image);

            Assert.AreEqual(image.Base64EncodedImage, imageDTO.Base64EncodedImage);
        }
    }
}
