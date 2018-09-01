using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Exceptions;
using WebTracking.Repository.Mappers;

namespace VehicleTracking.Repository
{
    public class UserDAOImp : UserDAO
    {

        private UserMapper userMapper;

        public UserDAOImp()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            userMapper = new UserMapper(roleDAO);
        }

        public void AddUser(UserDTO userDTO)
        {
            User user = this.userMapper.ToEntity(userDTO);
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                context.Roles.Attach(user.IdRole);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public UserDTO FindUserByUserName(string userName)
        {
            User user = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                user = context.Users
                    .Include("IdRole")
                    .Where(u => u.UserName == userName)
                    .ToList().FirstOrDefault();
            }

            UserDTO userDTO = null;
            if(user != null)
            {
                userDTO = this.userMapper.ToDTO(user);
            }

            return userDTO;
        }

        public UserDTO LogIn(LoginDTO loginUser)
        {
            User user = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                user = context.Users
                    .Include("IdRole")
                    .Where(u => u.UserName == loginUser.UserName && u.Password == loginUser.Password)
                    .ToList().FirstOrDefault();
            }

            UserDTO userDTO = null;

            if(user != null)
            {
                userDTO = this.userMapper.ToDTO(user);
            }

            return userDTO;
        }
    }
}
