using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Exceptions;

namespace WebTracking.Repository.Mappers
{
    public class UserMapper : Mapper<User, UserDTO>
    {
        private RoleDAO roleDAO;

        public UserMapper(RoleDAO roleDAO)
        {
            this.roleDAO = roleDAO;
        }

        public UserDTO ToDTO(User user)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.Name = user.Name;
            userDTO.LastName = user.LastName;
            userDTO.UserName = user.UserName;
            userDTO.Phone = user.Phone;
            userDTO.Role = user.IdRole.Name;

            return userDTO;
        }

        public User ToEntity(UserDTO userDTO)
        {
            RoleDAO roleDAO = new RoleDAOImp();
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Name = userDTO.Name;
            user.LastName = userDTO.LastName;
            user.UserName = userDTO.UserName;
            user.Password = userDTO.Password;
            user.Phone = userDTO.Phone;

            Role role = null;
            if (userDTO.Role.Equals(Roles.ADMINISTRATOR.GetName()))
            {
                role = roleDAO.FindRole(Roles.ADMINISTRATOR);
            }
            else if (userDTO.Role.Equals(Roles.CARRIER.GetName()))
            {
                role = roleDAO.FindRole(Roles.CARRIER);
            }
            else if (userDTO.Role.Equals(Roles.PORT_OPERATOR.GetName()))
            {
                role = roleDAO.FindRole(Roles.PORT_OPERATOR);
            }
            else if (userDTO.Role.Equals(Roles.YARD_OPERATOR.GetName()))
            {
                role = roleDAO.FindRole(Roles.YARD_OPERATOR);
            } else if (userDTO.Role.Equals(Roles.SELLER.GetName()))
            {
                role = roleDAO.FindRole(Roles.SELLER);
            } else
            {
                throw new IncorrectUserRoleException("El rol que ingreso para el usuario: " + userDTO.UserName + " no es correcto.");
            }

            user.IdRole = role;

            return user;
        }

    }
}
