using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.Services;
using Moq;
using VehicleTracking.Web.Api.Controllers;
using VehicleTracking.Web.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;
using VehicleTracking.Services.Exceptions;
using System.Web.Http.Results;

namespace VehicleTracking.Tests.Users
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            UserDTO user = createUserDTO();
            UserDTO userLogged = createUserDTO();
            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.AddUser(user)).Verifiable();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Returns(userLogged);
            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();
            this.addTokenHeaderToRequest(userController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)userController.Create(user);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateUserErrorTest()
        {
            UserDTO user = createUserDTO();

            Guid token = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.AddUser(user)).Verifiable();
            mockUserService.Setup(us => us.GetUserLoggedIn(token)).Throws(new UserNotExistException());

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();
            this.addTokenHeaderToRequest(userController.Request, token);

            ResponseMessageResult response = (ResponseMessageResult)userController.Create(user);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateUserWithoutTokenHeaderTest()
        {
            UserDTO user = createUserDTO();

            Guid token = Guid.NewGuid();

            UserController userController = new UserController(null);
            userController.Request = createUserControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)userController.Create(user);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void CreateUserInvalidTokenTest()
        {
            UserDTO user = createUserDTO();

            string invalidToken = "test";

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.AddUser(user)).Verifiable();

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();
            userController.Request.Headers.Add("UserToken", invalidToken);

            ResponseMessageResult response = (ResponseMessageResult)userController.Create(user);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void LoginSuccessfullyTest()
        {
            LoginDTO loginUser = createLoginDTO();

            UserDTO userDTO = createUserDTO();

            UserLoggedDTO userLoggedDTO = createUserLoggedDTO();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.LogIn(loginUser)).Returns(userLoggedDTO);
            mockUserService.Setup(us => us.GetUserLoggedIn(userLoggedDTO.Token)).Returns(userDTO);

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)userController.LogIn(loginUser);

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void LoginErrorTest()
        {
            LoginDTO loginUser = createLoginDTO();

            Guid id = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.LogIn(loginUser)).Throws(new UserOrPasswordNotFoundException());

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();

            ResponseMessageResult response = (ResponseMessageResult)userController.LogIn(loginUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        [TestMethod]
        public void LogoutSuccessfullyTest()
        {

            Guid id = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.LogOut(id)).Verifiable();

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();
            this.addTokenHeaderToRequest(userController.Request, id);

            ResponseMessageResult response = (ResponseMessageResult)userController.LogOut();

            Assert.AreEqual(HttpStatusCode.OK, response.Response.StatusCode);
        }

        [TestMethod]
        public void LogoutErrorTest()
        {
            Guid id = Guid.NewGuid();

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(us => us.LogOut(id)).Throws(new UserNotExistException());

            UserController userController = new UserController(mockUserService.Object);
            userController.Request = createUserControllerRequest();
            this.addTokenHeaderToRequest(userController.Request, id);

            ResponseMessageResult response = (ResponseMessageResult)userController.LogOut();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.Response.StatusCode);
        }

        private UserDTO createUserDTO()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Pepe";
            userDTO.LastName = "Perez";
            userDTO.UserName = "pepito123";
            userDTO.Phone = 091234567;
            userDTO.Password = "123456";
            userDTO.Role = "Administrador";

            return userDTO;
        }

        private UserLoggedDTO createUserLoggedDTO()
        {
            UserLoggedDTO userLoggedDTO = new UserLoggedDTO();
            userLoggedDTO.FullName = "Pepe Perez";
            userLoggedDTO.Role = "Administrador";
            userLoggedDTO.Token = Guid.NewGuid();

            return userLoggedDTO;
        }

        private LoginDTO createLoginDTO()
        {
            LoginDTO loginUser = new LoginDTO();
            loginUser.UserName = "pepito123";
            loginUser.Password = "123456";

            return loginUser;
        }

        private HttpRequestMessage createUserControllerRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return request;
        }

        private void addTokenHeaderToRequest(HttpRequestMessage request, Guid token)
        {
            request.Headers.Add("UserToken", token.ToString());
        }
    }
}
