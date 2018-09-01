using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;

namespace VehicleTracking.Repository
{
    public class Base64ImageDAOImp : Base64ImageDAO
    {
        public Base64ImageDAOImp()
        {
        }

        public Guid AddImage(Base64Image image)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                context.Images.Add(image);
                context.SaveChanges();
            }
            return image.Id;
        }

        public Base64Image FindImageByBase64Encode(string code)
        {
            Base64Image image = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                image = context.Images
                    .Where(i => i.Base64EncodedImage == code)
                    .ToList().FirstOrDefault();
            }
            return image;
        }
    }
}
