using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Handler;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class InspectionController : ApiController
    {
        private InspectionService inspectionService;
        private UserService userService;

        public InspectionController() : base() { }

        public InspectionController(InspectionService inspectionService, UserService userService)
        {
            this.userService = userService;
            this.inspectionService = inspectionService;
        }

        [Route("inspections")]
        public IHttpActionResult Post([FromBody]InspectionDTO inspection)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (inspection != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    inspection.CreatorUserName = user.UserName;
                    PermissionHandler permissionHandler = new PermissionHandler();
                    bool permissionInPort = permissionHandler.IsUserAllowedToCreateInspectionOnPort(user.Role);
                    bool permissionInYard = permissionHandler.IsUserAllowedToCreateInspectionOnYard(user.Role);
                    if ((permissionInPort && this.LocationIsPort(inspection.Location)) 
                        || (permissionInYard && this.LocationIsYard(inspection.Location)))
                    {
                        this.inspectionService.CreateInspection(inspection);
                        response = this.Request.CreateResponse(HttpStatusCode.OK);
                    } else if (this.LocationIsPort(inspection.Location) || this.LocationIsYard(inspection.Location))
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                    } else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El lugar ingresado no es un lugar válido");
                    }
                }
                catch (UserNotExistException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (ImageNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (FormatException)
                {
                    string message = "El token enviado no tiene un formato valido.";
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }
                catch (InvalidOperationException)
                {
                    string message = "No se ha enviado header de autenticación.";
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }
                catch (VehicleNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
            } else
            {
                string message = "El formato de usuario es incorrecto";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
            
            return ResponseMessage(response);
        }

        [Route("inspections")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                List<InspectionDTO> inspections = this.inspectionService.GetAllInspections();
                response = this.Request.CreateResponse(HttpStatusCode.OK, inspections);
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

            return ResponseMessage(response);
        }

        [Route("inspections/{id}")]
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Guid guidId;
            try
            {
                guidId = Guid.Parse(id);
            }
            catch (FormatException)
            {
                string message = "El id enviado no tiene un formato valido.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                return ResponseMessage(response);
            }
            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                InspectionDTO batch = this.inspectionService.FindInspectionById(guidId);
                response = this.Request.CreateResponse(HttpStatusCode.OK, batch);
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

            return ResponseMessage(response);
        }

        private Guid GetToken()
        {
            var headers = this.Request.Headers;
            string token = headers.GetValues("UserToken").First();

            return Guid.Parse(token);
        }

        private bool LocationIsYard(string location)
        {
            bool isInYard = false;
            if (location == "Patio 1" || location == "Patio 2" || location == "Patio 3")
            {
                isInYard = true;
            }
            return isInYard;
        }

        private bool LocationIsPort(string location)
        {
            bool isInPort = false;
            if (location == "Puerto 1" || location == "Puerto 2" || location == "Puerto 3")
            {
                isInPort = true;
            }
            return isInPort;
        }
    }
}
