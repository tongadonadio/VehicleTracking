using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;

namespace VehicleTracking.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class RoleController : ApiController
    {

        private RoleService roleService;

        public RoleController() : base() { }

        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        [Route("roles")]
        [HttpGet]
        public List<string> GetRoles()
        {
            this.roleService = new RoleServiceImp();
            return this.roleService.GetRoles();
        }
    }
}
