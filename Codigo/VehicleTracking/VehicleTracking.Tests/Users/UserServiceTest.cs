using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using Moq;
using VehicleTracking.Repository;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Tests.Users
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void CreateUserSuccessfullyTest()
        {
            Role role = new Role(Roles.ADMINISTRATOR);
            User user = new User("Carlos", "Perez", "Carlitos", 091234567, "Password", role);

            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Carlos";
            userDTO.LastName = "Perez";
            userDTO.UserName = "carlitosp81";
            userDTO.Phone = 091234567;
            userDTO.Password = "Password";
            userDTO.Role = "Administrador";

            var mockUserDAO = new Mock<UserDAO>();
            UserDTO mockUser = null;
            mockUserDAO.Setup(us => us.FindUserByUserName("Carlitos")).Returns(mockUser);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            userService.AddUser(userDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDuplicatedException))]
        public void ErrorCreatingUserWithSameUserNameTest()
        {
            Role role = new Role(Roles.ADMINISTRATOR);
            User user = new User("Carlos", "Perez", "Carlitos", 091234567, "Password", role);

            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Carlos";
            userDTO.LastName = "Perez";
            userDTO.UserName = "Carlitos";
            userDTO.Phone = 091234567;
            userDTO.Password = "Password";
            userDTO.Role = "Administrador";

            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(us => us.FindUserByUserName("Carlitos")).Returns(userDTO);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            userService.AddUser(userDTO);
        }

        [TestMethod]
        public void LogInSuccessfullyTest()
        {
            LoginDTO loginUser = new LoginDTO();
            loginUser.UserName = "pepito123";
            loginUser.Password = "123456";

            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Pepe";
            userDTO.LastName = "Perez";
            userDTO.UserName = "pepito123";
            userDTO.Phone = 091234567;
            userDTO.Password = "123456";
            userDTO.Role = "Administrador";

            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(us => us.LogIn(loginUser)).Returns(userDTO);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            UserLoggedDTO result = userService.LogIn(loginUser);
            userService.LogOut(result.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(UserOrPasswordNotFoundException))]
        public void LogInErrorTest()
        {
            LoginDTO loginUser = new LoginDTO();
            loginUser.UserName = "pepito123";
            loginUser.Password = "123456";

            var mockUserDAO = new Mock<UserDAO>();
            UserDTO userDTO = null;
            mockUserDAO.Setup(us => us.LogIn(loginUser)).Returns(userDTO);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            UserLoggedDTO result = userService.LogIn(loginUser);
        }

        [TestMethod]
        public void LoggedInUserTest()
        {
            LoginDTO loginUser = new LoginDTO();
            loginUser.UserName = "pepito123";
            loginUser.Password = "123456";

            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Pepe";
            userDTO.LastName = "Perez";
            userDTO.UserName = "pepito123";
            userDTO.Phone = 091234567;
            userDTO.Password = "123456";
            userDTO.Role = "Administrador";

            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(us => us.LogIn(loginUser)).Returns(userDTO);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            UserLoggedDTO result = userService.LogIn(loginUser);
            UserLoggedDTO newResult = userService.LogIn(loginUser);

            UserDTO resultUser = userService.GetUserLoggedIn(result.Token);

            Assert.AreEqual(userDTO.LastName, resultUser.LastName);
            Assert.AreEqual(userDTO.Name, resultUser.Name);
            Assert.AreEqual(userDTO.Phone, resultUser.Phone);
            Assert.AreEqual(userDTO.Role, resultUser.Role);
            Assert.AreEqual(userDTO.UserName, resultUser.UserName);
            userService.LogOut(result.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void UserNotLoggedInTest()
        {
            Guid id = Guid.NewGuid();
            var userService = new UserServiceImpl(null);

            UserDTO resultUser = userService.GetUserLoggedIn(id);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void LogOutUserTest()
        {
            LoginDTO loginUser = new LoginDTO();
            loginUser.UserName = "pepito123";
            loginUser.Password = "123456";

            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Pepe";
            userDTO.LastName = "Perez";
            userDTO.UserName = "pepito123";
            userDTO.Phone = 091234567;
            userDTO.Password = "123456";
            userDTO.Role = "Administrador";

            var mockUserDAO = new Mock<UserDAO>();
            mockUserDAO.Setup(us => us.LogIn(loginUser)).Returns(userDTO);

            var userService = new UserServiceImpl(mockUserDAO.Object);

            UserLoggedDTO result = userService.LogIn(loginUser);
            userService.LogOut(result.Token);

            UserDTO resultUser = userService.GetUserLoggedIn(result.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void LogOutUserErrorTest()
        {
            Guid id = Guid.NewGuid();
            var userService = new UserServiceImpl(null);
            userService.LogOut(id);
        }

    }
}
