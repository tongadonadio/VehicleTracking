using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository;
using WebTracking.Repository.Mappers;
using WebTracking.Repository.Exceptions;

namespace VehicleTracking.Tests.Users
{
    [TestClass]
    public class UserMapperTest
    {
        [TestMethod]
        public void MapUserDTOAdminToUserTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.Role = "Administrador";
            User userEntity = mapper.ToEntity(user);

            Assert.AreEqual(user.LastName, userEntity.LastName);
            Assert.AreEqual(user.Name, userEntity.Name);
            Assert.AreEqual(user.Password, userEntity.Password);
            Assert.AreEqual(user.Phone, userEntity.Phone);
            Assert.AreEqual(user.Role, userEntity.IdRole.Name);
            Assert.AreEqual(user.UserName, userEntity.UserName);
        }

        [TestMethod]
        public void MapUserDTOPortOperatorToUserTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.Role = "Operario del Puerto";
            User userEntity = mapper.ToEntity(user);

            Assert.AreEqual(user.LastName, userEntity.LastName);
            Assert.AreEqual(user.Name, userEntity.Name);
            Assert.AreEqual(user.Password, userEntity.Password);
            Assert.AreEqual(user.Phone, userEntity.Phone);
            Assert.AreEqual(user.Role, userEntity.IdRole.Name);
            Assert.AreEqual(user.UserName, userEntity.UserName);
        }

        [TestMethod]
        public void MapUserDTOCarrierToUserTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.Role = "Transportista";
            User userEntity = mapper.ToEntity(user);

            Assert.AreEqual(user.LastName, userEntity.LastName);
            Assert.AreEqual(user.Name, userEntity.Name);
            Assert.AreEqual(user.Password, userEntity.Password);
            Assert.AreEqual(user.Phone, userEntity.Phone);
            Assert.AreEqual(user.Role, userEntity.IdRole.Name);
            Assert.AreEqual(user.UserName, userEntity.UserName);
        }

        [TestMethod]
        public void MapUserDTOYardOperatorToUserTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.Role = "Operario del Patio";
            User userEntity = mapper.ToEntity(user);

            Assert.AreEqual(user.LastName, userEntity.LastName);
            Assert.AreEqual(user.Name, userEntity.Name);
            Assert.AreEqual(user.Password, userEntity.Password);
            Assert.AreEqual(user.Phone, userEntity.Phone);
            Assert.AreEqual(user.Role, userEntity.IdRole.Name);
            Assert.AreEqual(user.UserName, userEntity.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectUserRoleException))]
        public void ErrorMappingUserDTOToUserWhenRoleIsNotCorrect()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            UserDTO user = new UserDTO();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.Role = "Test";
            User userEntity = mapper.ToEntity(user);
        }

        [TestMethod]
        public void MappingUserToUserDTOPasswordNullTest()
        {
            RoleDAO roleDAO = new RoleDAOImp();
            UserMapper mapper = new UserMapper(roleDAO);
            User user = new User();
            user.Name = "Carlos";
            user.LastName = "Perez";
            user.UserName = "carlitosp81";
            user.Phone = 091234567;
            user.Password = "Password";
            user.IdRole = new Role(Roles.CARRIER);

            UserDTO userDTO = mapper.ToDTO(user);

            Assert.AreEqual(null, userDTO.Password);
        }
    }
}
