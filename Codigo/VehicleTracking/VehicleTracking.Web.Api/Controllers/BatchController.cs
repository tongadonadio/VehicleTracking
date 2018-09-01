using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
    public class BatchController : ApiController
    {
        private BatchService batchService;
        private UserService userService;

        public BatchController() : base() { }

        public BatchController(BatchService batchService, UserService userService)
        {
            this.batchService = batchService;
            this.userService = userService;
        }

        [Route("batches")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToListBatches(user.Role))
                {
                    List<BatchDTO> batches = this.batchService.GetAllBatches();
                    response = this.Request.CreateResponse(HttpStatusCode.OK, batches);
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

        [Route("batches/{id}")]
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
                if (permissionHandler.IsUserAllowedToListBatches(user.Role))
                {
                    BatchDTO batch = this.batchService.FindBatchById(guidId);
                    response = this.Request.CreateResponse(HttpStatusCode.OK, batch);
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

        [Route("batches")]
        public IHttpActionResult Post([FromBody]BatchDTO batch)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Guid token = this.GetToken();
                UserDTO user = this.userService.GetUserLoggedIn(token);
                batch.CreatorUserName = user.UserName;
                PermissionHandler permissionHandler = new PermissionHandler();
                if (permissionHandler.IsUserAllowedToCreateBatch(user.Role))
                {
                    this.batchService.CreateBatch(batch);
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
            catch (VehicleInOtherBatchException e)
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
            catch (VehicleNotFoundException e)
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
