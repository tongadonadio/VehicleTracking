using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface UserDAO
    {
        UserDTO FindUserByUserName(string userName);

        void AddUser(UserDTO user);

        UserDTO LogIn(LoginDTO loginUser);
    }
}
