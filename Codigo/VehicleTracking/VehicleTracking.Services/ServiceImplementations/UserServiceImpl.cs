using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using VehicleTracking.Services.Container;
using VehicleTracking.Services.Log;
using VehicleTracking.Log.Events;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class UserServiceImpl : UserService
    {
        private UserDAO userDAO { get; set; }

        public UserServiceImpl(UserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        public void AddUser(UserDTO user)
        {
            if (this.IsUserAvailable(user.UserName))
            {
                this.userDAO.AddUser(user);
            }
            else
            {
                throw new UserDuplicatedException("No es posible crear un usuario con este userName, el userName: " + user.UserName + " ya se encuentra registrado.");
            }
        }

        public bool IsUserAvailable(string userName)
        {

            UserDTO user = this.userDAO.FindUserByUserName(userName);
            if (user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public UserLoggedDTO LogIn(LoginDTO loginUser)
        {
            UserLoggedDTO userLoggedDTO = new UserLoggedDTO();
            
            bool userLoggedIn = false;

            foreach(UserDTO user in TokenContainer.GetContext().LoggedUsers.Values)
            {
                if (user.UserName.Equals(loginUser.UserName))
                {
                    KeyValuePair<Guid, UserDTO> valuePair = TokenContainer.GetContext().LoggedUsers.FirstOrDefault(u => u.Value.Equals(user));
                    UserDTO userDTO = valuePair.Value;
                    userLoggedDTO.FullName = userDTO.Name + " " + userDTO.LastName;
                    userLoggedDTO.Role = userDTO.Role;
                    userLoggedDTO.Token = valuePair.Key;
                    userLoggedIn = true;

                    LogEvent log = new LoginEvent(user);
                    VehicleTrackingLog.GetInstance().WriteEvent(log);
                }
            }

            if (!userLoggedIn)
            {
                UserDTO userDTO = this.userDAO.LogIn(loginUser);

                if (userDTO == null)
                {
                    throw new UserOrPasswordNotFoundException("El nombre de usuario o password no son correctos");
                }
                else
                {
                    Guid token = Guid.NewGuid();
                    TokenContainer.GetContext().LoggedUsers.Add(token, userDTO);
                    userLoggedDTO.Token = token;
                    userLoggedDTO.FullName = userDTO.Name + " " + userDTO.LastName;
                    userLoggedDTO.Role = userDTO.Role;

                    LogEvent log = new LoginEvent(userDTO);
                    VehicleTrackingLog.GetInstance().WriteEvent(log);
                }
            }

            return userLoggedDTO;
        }

        public UserDTO FindUserByUserName(string userName)
        {
            return this.userDAO.FindUserByUserName(userName);
        }

        public UserDTO GetUserLoggedIn(Guid id)
        {
            UserDTO userDTO = null;

            if(TokenContainer.GetContext().LoggedUsers.ContainsKey(id))
            {
                userDTO = TokenContainer.GetContext().LoggedUsers[id];
            } else
            {
                throw new UserNotExistException("El usuario no se encuentra loggeado en el sistema.");
            } 

            return userDTO;
        }

        public void LogOut(Guid id)
        {
            if (TokenContainer.GetContext().LoggedUsers.ContainsKey(id))
            {
                TokenContainer.GetContext().LoggedUsers.Remove(id);
            } else
            {
                throw new UserNotExistException("El usuario no se encuentra loggeado en el sistema.");
            }
        }
    }
}
