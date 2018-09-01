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
    public class ZoneController : ApiController
    {

        private UserService userService;
        private ZoneService zoneService;
        private FlowService flowService;

        public ZoneController(UserService userService, ZoneService zoneService, FlowService flowService)
        {
            this.userService = userService;
            this.zoneService = zoneService;
            this.flowService = flowService;
        }

        [Route("zones")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] ZoneDTO zone)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToCreateZones(userLogged.Role))
                {
                    this.zoneService.AddZone(zone);
                    response = this.Request.CreateResponse(HttpStatusCode.Created);
                } else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (ZoneNullAttributesException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (UserNotExistException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (ZoneOutOfCapacityException e)
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

        [Route("zones/{id}")]
        [HttpPost]
        public IHttpActionResult CreateSubZone(string id, [FromBody] ZoneDTO zone)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                Guid mainZoneId = Guid.Parse(id);
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToCreateZones(userLogged.Role))
                {
                    this.zoneService.AddSubZone(mainZoneId, zone);
                    response = this.Request.CreateResponse(HttpStatusCode.Created);
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (ZoneNullAttributesException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (UserNotExistException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (ZoneOutOfCapacityException e)
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

        [Route("zones/vehicle/assign")]
        [HttpPost]
        public IHttpActionResult AssignVehicle([FromUri] string zoneId, [FromUri] string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Guid guidZone;

            try
            {
                guidZone = Guid.Parse(zoneId);
            }
            catch (FormatException)
            {
                string message = "El id de zona no tiene un formato valido.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                return ResponseMessage(response);
            }

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToLocateVehiclesOnZones(userLogged.Role))
                {
                    this.zoneService.AssignVehicle(guidZone, vin);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                } else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (AssignVehicleToMainZoneException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (AssignVehicleToZoneWithoutCapacityException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
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

        [Route("zones/subzone/assign")]
        [HttpPost]
        public IHttpActionResult AssignZone([FromUri] string subZoneId, [FromUri] string zoneId)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Guid guidZone;
            Guid guidSubZone;

            try
            {
                guidZone = Guid.Parse(zoneId);
                guidSubZone = Guid.Parse(subZoneId);
            }
            catch (FormatException)
            {
                string message = "El id de zona o subzone no tiene un formato valido.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
                return ResponseMessage(response);
            }

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToMoveSubZonesToZones(userLogged.Role))
                {
                    this.zoneService.AssignZone(guidSubZone, guidZone);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                } else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (ZoneCannotBeSubzoneOfAnotherSubzoneException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (ZoneOutOfCapacityException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
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

        [Route("zones")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToListZones(user.Role))
                {
                    List<ZoneDTO> zones = this.zoneService.GetAllZones();
                    response = this.Request.CreateResponse(HttpStatusCode.OK, zones);
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

            return ResponseMessage(response);
        }

        [Route("zones/steps")]
        [HttpGet]
        public IHttpActionResult GetAllSteps()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                List<FlowStepDTO> flowSteps = this.flowService.GetAllSteps();
                response = this.Request.CreateResponse(HttpStatusCode.OK, flowSteps);
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

        [Route("zones/flow")]
        [HttpGet]
        public IHttpActionResult GetFlow()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                List<FlowItemDTO> flow = this.flowService.GetFlow();
                response = this.Request.CreateResponse(HttpStatusCode.OK, flow);
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

        [Route("zones/{id}")]
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
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToListZones(user.Role))
                {
                    ZoneDTO zone = this.zoneService.FindZoneById(guidId);
                    response = this.Request.CreateResponse(HttpStatusCode.OK, zone);
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

            return ResponseMessage(response);
        }

        [Route("zones/vehicle/{vin}")]
        [HttpDelete]
        public IHttpActionResult RemoveVehicle(string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (vin != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToLocateVehiclesOnZones(userLogged.Role))
                    {
                        this.zoneService.RemoveVehicle(vin);
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
            } else
            {
                string message = "El formato de vin ingresado es incorrecto.";
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

    }
}
