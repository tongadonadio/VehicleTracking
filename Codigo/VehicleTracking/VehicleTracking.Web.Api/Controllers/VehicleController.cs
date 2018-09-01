using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTracking.Services;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Handler;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class VehicleController : ApiController
    {

        private UserService userService;
        private VehicleService vehicleService;
        private HistoryService historyService;

        public VehicleController(UserService userService, VehicleService vehicleService, HistoryService historyService)
        {
            this.userService = userService;
            this.vehicleService = vehicleService;
            this.historyService = historyService;
        }

        [Route("vehicles")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                List<VehicleDTO> vehicles = this.vehicleService.GetAllVehicles();
                List<VehicleDTO> vehiclesAllowed = GetVehiclesAllowed(vehicles, userLogged.Role);

                response = this.Request.CreateResponse(HttpStatusCode.OK, vehiclesAllowed);
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

        [Route("vehicles/{vin}")]
        [HttpGet]
        public IHttpActionResult Get(string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                VehicleDTO vehicleDTO = this.vehicleService.FindVehicleByVin(vin);
                if (permissionHandler.IsUserAllowedToListVehicle(userLogged.Role, vehicleDTO.Status))
                {

                    response = this.Request.CreateResponse(HttpStatusCode.OK, vehicleDTO);
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (VehicleNotFoundException e)
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

        [Route("vehicles/group/{state}")]
        [HttpGet]
        public IHttpActionResult GetByGroup(string state)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO userLogged = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToCreateVehicle(userLogged.Role))
                {
                    List<VehicleDTO> vehicles = this.vehicleService.GetAllVehicles();
                    List<VehicleDTO> vehiclesGroup = GetVehiclesGroup(vehicles, state);
                    response = this.Request.CreateResponse(HttpStatusCode.OK, vehiclesGroup);
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

        [Route("vehicles")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] VehicleDTO vehicle)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (vehicle != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToCreateVehicle(user.Role))
                    {
                        this.vehicleService.AddVehicle(vehicle);
                        response = this.Request.CreateResponse(HttpStatusCode.Created);
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                    }
                }
                catch (VehicleNullAttributesException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (VehicleVinDuplicatedException e)
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
            } else
            {
                string message = "El formato del vehículo es incorrecto.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }


            return ResponseMessage(response);
        }

        [Route("vehicles/{vin}")]
        [HttpDelete]
        public IHttpActionResult Delete(string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToDeleteVehicle(user.Role))
                {
                    this.vehicleService.DeleteVehicle(vin);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                } else
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
            catch (VehicleNotFoundException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (FormatException)
            {
                string message = "El token enviado no tiene un formato valido.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }

            return ResponseMessage(response);
        }

        [Route("vehicles/{vin}")]
        [HttpPut]
        public IHttpActionResult Update(string vin, [FromBody] VehicleDTO vehicle)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToUpdateVehicle(user.Role))
                {
                    vehicle.Vin = vin;
                    this.vehicleService.UpdateVehicle(vehicle);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                } else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (VehicleNotFoundException e)
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

        [Route("vehicles/{vin}/history")]
        [HttpGet]
        public IHttpActionResult History(string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                this.userService.GetUserLoggedIn(token);
                VehicleHistoryDTO historyDTO = this.historyService.GetHistory(vin);
                this.vehicleService.FindVehicleByVin(vin);
                response = this.Request.CreateResponse(HttpStatusCode.OK, historyDTO);
            }
            catch (VehicleNotFoundException e)
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

        [Route("vehicles/{vin}/ready")]
        [HttpPost]
        public IHttpActionResult SetVehicleReadyToSell([FromUri] string vin)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (vin != "")
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToSetVehicleReadyToSell(user.Role))
                    {
                        this.vehicleService.SetVehicleReadyToSell(vin);
                        response = this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                    }
                }
                catch (VehicleNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (ZoneNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (FlowStepOrderException e)
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
            }
            else
            {
                string message = "El vin no puede ser vacio.";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }


            return ResponseMessage(response);
        }

        [Route("vehicles/sell")]
        [HttpPut]
        public IHttpActionResult SellVehicle([FromBody] VehicleDTO vehicle)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToSellVehicle(user.Role))
                {
                    this.vehicleService.SellVehicle(vehicle);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (VehicleNotFoundException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (VehicleNullAttributesException e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (FlowStepOrderException e)
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


        private Guid GetToken()
        {
            var headers = this.Request.Headers;
            string token = headers.GetValues("UserToken").First();

            return Guid.Parse(token);
        }

        private List<VehicleDTO> GetVehiclesAllowed(List<VehicleDTO> vehicles, string role)
        {
            List<VehicleDTO> result = new List<VehicleDTO>();
            if (role.Equals("Administrador"))
            {
                result = vehicles;
            }else
            {
                foreach (VehicleDTO vehicle in vehicles)
                {
                    if ((vehicle.Status.Equals(Models.StatusCode.InPort) || vehicle.Status.Equals(Models.StatusCode.InspectedInPort)) && role.Equals(RolesDTO.PORT_OPERATOR)
                        || vehicle.Status.Equals(Models.StatusCode.ReadyToGo) && role.Equals(RolesDTO.PORT_OPERATOR)
                        || vehicle.Status.Equals(Models.StatusCode.Waiting) && role.Equals(RolesDTO.YARD_OPERATOR)
                        || vehicle.Status.Equals(Models.StatusCode.ReadyToBeLocated) && role.Equals(RolesDTO.YARD_OPERATOR)
                        || vehicle.Status.Equals(Models.StatusCode.ReadyToSell) && role.Equals(RolesDTO.SELLER)
                        || vehicle.Status.Equals(Models.StatusCode.Sold) && role.Equals(RolesDTO.SELLER))
                    {
                        result.Add(vehicle);
                    }
                }
            }
            return result;
        }

        private List<VehicleDTO> GetVehiclesGroup(List<VehicleDTO> vehicles, string state)
        {
            List<VehicleDTO> result = new List<VehicleDTO>();
            foreach (VehicleDTO vehicle in vehicles)
            {
                if (vehicle.Status.Equals(state))
                {
                    result.Add(vehicle);
                }
            }
            return result;
        }

    }
}
