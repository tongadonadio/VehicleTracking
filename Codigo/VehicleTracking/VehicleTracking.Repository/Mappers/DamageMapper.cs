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
    public class DamageMapper : Mapper<Damage, DamageDTO>
    {
        public DamageMapper(){ }
        
        public DamageDTO ToDTO(Damage damage)
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
            return damageDTO;
        }

        public Damage ToEntity(DamageDTO damageDTO)
        {
            Damage damage = new Damage();
            damage.Description = damageDTO.Description;
            damage.Images = new List<Base64Image>();
            
            foreach (var imageDTO in damageDTO.Images)
            {
                Base64Image image = new Base64Image();
                image.Base64EncodedImage = imageDTO.Base64EncodedImage;
                damage.Images.Add(image);
            }

            return damage;
        }
    }
}
