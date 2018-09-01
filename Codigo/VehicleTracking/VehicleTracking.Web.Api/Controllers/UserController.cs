using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Handler;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class UserController : ApiController
    {

        private UserService userService;

        public UserController() : base() { }

        public UserController(UserService userService)
        {
            this.userService = userService;
            this.Request = new HttpRequestMessage();
        }

        [Route("users")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] UserDTO user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (user != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToCreateUser(userLogged.Role))
                    {
                        this.userService.AddUser(user);
                        response = this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                    }
                }
                catch (UserNotExistException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (InvalidOperationException)
                {
                    string message = "No se ha enviado header de autenticación.";
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }
                catch (FormatException)
                {
                    string message = "El token enviado no tiene un formato valido.";
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }
            }else
            {
                string message = "El formato de usuario es incorrecto";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
            

            return ResponseMessage(response);
        }

        [Route("users/login")]
        [HttpPost]
        public IHttpActionResult LogIn([FromBody] LoginDTO user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                UserLoggedDTO userDTO = this.userService.LogIn(user);
                response = this.Request.CreateResponse(HttpStatusCode.OK, userDTO);
            } catch(UserOrPasswordNotFoundException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            return ResponseMessage(response);
        }

        [Route("users/logout")]
        [HttpPost]
        public IHttpActionResult LogOut()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                Guid token = this.GetToken();
                this.userService.LogOut(token);
                response = this.Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (UserNotExistException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return ResponseMessage(response);
        }

        private Guid GetToken()
        {
            var headers = this.Request.Headers;
            string token = headers.GetValues("UserToken").First();

            return Guid.Parse(token);
        }
    }
}
