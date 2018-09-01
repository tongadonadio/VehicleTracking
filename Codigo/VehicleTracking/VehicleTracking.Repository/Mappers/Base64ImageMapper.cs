using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace VehicleTracking.Repository.Mappers
{
    public class Base64ImageMapper : Mapper<Base64Image, Base64ImageDTO>
    {
        Base64ImageDAO imageDAO;

        public Base64ImageMapper(Base64ImageDAO imageDAO)
        {
            this.imageDAO = imageDAO;
        }

        public Base64ImageDTO ToDTO(Base64Image image)
        {
            Base64ImageDTO imageDTO = new Base64ImageDTO();
            imageDTO.Base64EncodedImage = image.Base64EncodedImage;
            return imageDTO;
        }

        public Base64Image ToEntity(Base64ImageDTO imageDTO)
        {
            Base64Image image = new Base64Image();
            image.Base64EncodedImage = imageDTO.Base64EncodedImage;
            return image;
        }
    }
}
