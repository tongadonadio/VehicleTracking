using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface UserService
    {
        bool IsUserAvailable(string userName);

        void AddUser(UserDTO user);

        UserLoggedDTO LogIn(LoginDTO loginUser);

        UserDTO GetUserLoggedIn(Guid id);

        UserDTO FindUserByUserName(string userName);

        void LogOut(Guid id);
    }
}
