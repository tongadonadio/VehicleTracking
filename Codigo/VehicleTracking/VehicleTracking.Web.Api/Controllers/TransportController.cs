using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Handler;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class TransportController : ApiController
    {
        private TransportService transportService;
        private UserService userService;

        public TransportController() : base() { }

        public TransportController(TransportService transportService, UserService userService)
        {
            this.transportService = transportService;
            this.userService = userService;
        }

        [Route("transports")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {

                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToListTransports(user.Role))
                {
                    List<TransportDTO> transports = this.transportService.GetAllTransports();
                    response = this.Request.CreateResponse(HttpStatusCode.OK, transports);
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                }
            }
            catch (Exception e)
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            return ResponseMessage(response);
        }

        [Route("transports/start")]
        [HttpPost]
        public IHttpActionResult Start([FromBody]List<Guid> batchesId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            
            if (batchesId != null && batchesId.Count > 0)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToTransportBatches(user.Role))
                    {
                        this.transportService.StartTransport(batchesId, user);
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
                catch (BatchNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                catch (BatchIsNotReadyException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
            }else
            {
                string message = "El formato de los lotes es incorrecto";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }

            return ResponseMessage(response);
        }

        [Route("transports/{id}/finish")]
        [HttpPut]
        public IHttpActionResult Finish([FromUri]Guid id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (id != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToFinishTransportation(user.Role))
                    {
                        this.transportService.FinishTransport(id);
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
                catch (TransportNotFoundException e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
                
            } else
            {
                string message = "El formato del transporte es incorrecto";
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
            

            return ResponseMessage(response);
        }

        [Route("transports/{id}")]
        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (id != null)
            {
                try
                {
                    Guid token = this.GetToken();
                    UserDTO user = this.userService.GetUserLoggedIn(token);
                    PermissionHandler permissionHandler = new PermissionHandler();
                    if (permissionHandler.IsUserAllowedToListTransports(user.Role))
                    {
                        TransportDTO transport = this.transportService.FindTransportById(id);
                        response = this.Request.CreateResponse(HttpStatusCode.OK, transport);
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.Unauthorized, "El usuario no tiene permisos para ejecutar esta accion");
                    }
                }
                catch (Exception e)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                }
            }
            else
            {
                string message = "El formato del transporte es incorrecto";
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
