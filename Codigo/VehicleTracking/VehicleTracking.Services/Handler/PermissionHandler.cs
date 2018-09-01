using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Handler
{
    public class PermissionHandler
    {

        private readonly string administrator = "Administrador";
        private readonly string portOperator = "Operario del Puerto";
        private readonly string carrier = "Transportista";
        private readonly string yardOperator = "Operario del Patio";
        private readonly string seller = "Vendedor";

        public bool IsUserAllowedToCreateBatch(string role)
        {
            return administrator.Equals(role) || portOperator.Equals(role);
        }

        public bool IsUserAllowedToListBatches(string role)
        {
            return administrator.Equals(role) || portOperator.Equals(role) || carrier.Equals(role);
        }

        public bool IsUserAllowedToCreateUser(string role)
        {
            return administrator.Equals(role);
        }

        public bool IsUserAllowedToCreateVehicle(string role)
        {
            return administrator.Equals(role);
        }

        public bool IsUserAllowedToDeleteVehicle(string role)
        {
            return administrator.Equals(role);
        }

        public bool IsUserAllowedToUpdateVehicle(string role)
        {
            return administrator.Equals(role);
        }

        public bool IsUserAllowedToListVehicle(string role, string vehicleStatus)
        {
            return administrator.Equals(role) || portOperator.Equals(role) && (vehicleStatus.Equals(StatusCode.InPort) || vehicleStatus.Equals(StatusCode.ReadyToGo))
                || yardOperator.Equals(role) && (vehicleStatus.Equals(StatusCode.Waiting) || vehicleStatus.Equals(StatusCode.ReadyToBeLocated));
        }

        public bool IsUserAllowedToCreateInspectionOnPort(string role)
        {
            return administrator.Equals(role) || portOperator.Equals(role);
        }

        public bool IsUserAllowedToTransportBatches(string role)
        {
            return administrator.Equals(role) || carrier.Equals(role);
        }

        public bool IsUserAllowedToFinishTransportation(string role)
        {
            return administrator.Equals(role) || carrier.Equals(role);
        }

        public bool IsUserAllowedToListTransports(string role)
        {
            return administrator.Equals(role) || carrier.Equals(role);
        }

        public bool IsUserAllowedToCreateInspectionOnYard(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToLocateVehiclesOnZones(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToCreateZones(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToMoveSubZonesToZones(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToListZones(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToSetVehicleReadyToSell(string role)
        {
            return administrator.Equals(role) || yardOperator.Equals(role);
        }

        public bool IsUserAllowedToSellVehicle(string role)
        {
            return administrator.Equals(role) || seller.Equals(role);
        }

        public bool IsUserAllowedToListRoles(string role)
        {
            return administrator.Equals(role);
        }

    }
}
