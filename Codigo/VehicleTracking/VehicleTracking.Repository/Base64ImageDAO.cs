using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface Base64ImageDAO
    {
        Guid AddImage(Base64Image image);

        Base64Image FindImageByBase64Encode(string code);
    }
}
