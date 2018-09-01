using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Exceptions;

namespace VehicleTracking.Tests.Users
{
    [TestClass]
    public class UserDAOTest
    {
        [TestMethod]
        public void PersistUserTest()
        {
            UserDTO expectedUser = new UserDTO();

            expectedUser.Name = "Pepe";
            expectedUser.LastName = "Lopez";
            expectedUser.UserName = "pepito123";
            expectedUser.Password = "PasswordTest";
            expectedUser.Phone = 091234567;
            expectedUser.Role = "Administrador";

            UserDAO userDAO = new UserDAOImp();
            userDAO.AddUser(expectedUser);
            UserDTO resultUser = userDAO.FindUserByUserName("pepito123");

            Assert.AreEqual(expectedUser.LastName, resultUser.LastName);
            Assert.AreEqual(expectedUser.Name, resultUser.Name);
            Assert.AreEqual(expectedUser.Phone, resultUser.Phone);
            Assert.AreEqual(expectedUser.Role, resultUser.Role);
            Assert.AreEqual(expectedUser.UserName, resultUser.UserName);
        }

        [TestMethod]
        public void LogInSuccessfullyTest()
        {
            UserDTO user = new UserDTO();

            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = "Administrador";

            UserDAO userDAO = new UserDAOImp();
            userDAO.AddUser(user);

            LoginDTO login = new LoginDTO();
            login.UserName = "pepito123";
            login.Password = "PasswordTest";

            UserDTO userDTO = userDAO.LogIn(login);

            Assert.IsNotNull(userDTO);
        }

        [TestMethod]
        public void LogInPasswordNotCorrectTest()
        {
            UserDTO user = new UserDTO();

            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = "Administrador";

            UserDAO userDAO = new UserDAOImp();
            userDAO.AddUser(user);

            LoginDTO login = new LoginDTO();
            login.UserName = "pepito123";
            login.Password = "test";

            UserDTO userDTO = userDAO.LogIn(login);

            Assert.IsNull(userDTO);
        }

        [TestMethod]
        public void LogInUserNotCorrectTest()
        {
            UserDTO user = new UserDTO();

            user.Name = "Pepe";
            user.LastName = "Lopez";
            user.UserName = "pepito123";
            user.Password = "PasswordTest";
            user.Phone = 091234567;
            user.Role = "Administrador";

            UserDAO userDAO = new UserDAOImp();
            userDAO.AddUser(user);

            LoginDTO login = new LoginDTO();
            login.UserName = "pe123";
            login.Password = "PasswordTest";

            UserDTO userDTO = userDAO.LogIn(login);

            Assert.IsNull(userDTO);
        }
    }
}
