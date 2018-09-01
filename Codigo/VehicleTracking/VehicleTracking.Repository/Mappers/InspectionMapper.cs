using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Repository.Mappers;
using VehicleTracking.Web.Models;

namespace WebTracking.Repository.Mappers
{
    public class InspectionMapper : Mapper<Inspection, InspectionDTO>
    {
        public InspectionMapper(){ }

        public InspectionDTO ToDTO(Inspection inspection)
        {
            InspectionDTO inspectionDTO = new InspectionDTO();

            inspectionDTO.CreatorUserName = inspection.IdUser.UserName;
            inspectionDTO.Date = inspection.Date;
            inspectionDTO.Location = inspection.IdLocation.Name;
            inspectionDTO.Id = inspection.Id;
            inspectionDTO.IdVehicle = inspection.IdVehicle.Vin;

            if (inspection.Damages != null)
            {
                List<DamageDTO> damagesDTO = new List<DamageDTO>();
                foreach (Damage damage in inspection.Damages)
                {
                    DamageDTO damageDTO = new DamageDTO();
                    damageDTO.Description = damage.Description;
                    damageDTO.Images = new List<Base64ImageDTO>();
                    foreach (Base64Image image in damage.Images)
                    {
                        Base64ImageDTO imageDTO = new Base64ImageDTO();
                        imageDTO.Base64EncodedImage = image.Base64EncodedImage;
                        damageDTO.Images.Add(imageDTO);
                    }
                    damagesDTO.Add(damageDTO);
                }
                inspectionDTO.Damages = damagesDTO;
            }

            return inspectionDTO;
        }

        public Inspection ToEntity(InspectionDTO inspectionDTO)
        {
            Location location = GetLocation(inspectionDTO.Location);

            Inspection inspection = new Inspection();
            inspection.Date = inspectionDTO.Date;
            inspection.Id = inspectionDTO.Id;
            inspection.IdLocation = location;

            User user = new User();
            user.UserName = inspectionDTO.CreatorUserName;

            Vehicle vehicle = new Vehicle();
            vehicle.Vin = inspectionDTO.IdVehicle;

            inspection.IdUser = user;
            inspection.IdVehicle = vehicle;

            if (inspectionDTO.Damages != null)
            {
                List<Damage> damages = new List<Damage>();
                foreach (DamageDTO damageDTO in inspectionDTO.Damages)
                {
                    Damage damage = new Damage();
                    damage.Description = damageDTO.Description;
                    damage.Images = new List<Base64Image>();
                    foreach (Base64ImageDTO imageDTO in damageDTO.Images)
                    {
                        Base64Image image = new Base64Image();
                        image.Base64EncodedImage = imageDTO.Base64EncodedImage;
                        damage.Images.Add(image);
                    }
                    damages.Add(damage);
                }
                inspection.Damages = damages;
            }

            return inspection;
        }

        private Location GetLocation(string name)
        {
            Location location = null;

            if (name.Equals(Ports.FIRST_PORT.GetName()))
            {
                location = new Location(Ports.FIRST_PORT);
            } else if(name.Equals(Ports.SECOND_PORT.GetName()))
            {
                location = new Location(Ports.SECOND_PORT);
            } else if (name.Equals(Ports.THIRD_PORT.GetName()))
            {
                location = new Location(Ports.THIRD_PORT);
            } else if (name.Equals(Yards.FIRST_YARD.GetName()))
            {
                location = new Location(Yards.FIRST_YARD);
            } else if (name.Equals(Yards.SECOND_YARD.GetName()))
            {
                location = new Location(Yards.SECOND_YARD);
            } else if (name.Equals(Yards.THIRD_YARD.GetName()))
            {
                location = new Location(Yards.THIRD_YARD);
            }

            return location;
        }
    }
}
