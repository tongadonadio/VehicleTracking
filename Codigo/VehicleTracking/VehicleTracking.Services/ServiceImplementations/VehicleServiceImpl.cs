using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class VehicleServiceImpl : VehicleService
    {

        private VehicleDAO vehicleDAO { get; set; }
        private FlowDAO flowDAO { get; set; }
        private ZoneDAO zoneDAO { get; set; }

        public VehicleServiceImpl(VehicleDAO vehicleDAO, FlowDAO flowDAO, ZoneDAO zoneDAO)
        {
            this.vehicleDAO = vehicleDAO;
            this.flowDAO = flowDAO;
            this.zoneDAO = zoneDAO;
        }

        public void AddVehicle(VehicleDTO vehicle)
        {
            if (this.IsVinAvailable(vehicle.Vin) && !this.ContainsNullAttributes(vehicle))
            {
                vehicle.Status = StatusCode.InPort;
                vehicle.CurrentLocation = null;
                this.vehicleDAO.AddVehicle(vehicle);
            }
            else if(this.ContainsNullAttributes(vehicle))
            {
                throw new VehicleNullAttributesException("Faltan datos en el vehiculo que esta intentando ingresar");
            }
            else
            {
                throw new VehicleVinDuplicatedException("No es posible crear un vehiculo con este vin, el vin: " + vehicle.Vin + " ya se encuentra registrado.");
            }
        }

        public bool IsVinAvailable(string vin)
        {
            VehicleDTO vehicle = this.vehicleDAO.FindVehicleByVin(vin);
            if(vehicle != null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool IsAssigned(string vin)
        {
            bool isAssigned = this.vehicleDAO.IsAssigned(vin);
            return isAssigned;
        }

        public void DeleteVehicle(string vin)
        {
            VehicleDTO vehicle = this.FindVehicleByVin(vin);
            this.vehicleDAO.DeleteVehicleByVin(vehicle.Vin);
        }

        public void UpdateVehicle(VehicleDTO vehicle)
        {
            VehicleDTO vehicleDTO = this.FindVehicleByVin(vehicle.Vin);
            this.vehicleDAO.UpdateVehicle(vehicle);
        }

        public VehicleDTO FindVehicleByVin(string vin)
        {
            VehicleDTO dto = this.vehicleDAO.FindVehicleByVin(vin);
            if(dto == null)
            {
                throw new VehicleNotFoundException("El vehiculo con vin: " + vin + " no esta registrado en el sistema.");
            }
            return dto;
        }

        public List<VehicleDTO> GetAllVehicles()
        {
            return this.vehicleDAO.GetAllVehicles();
        }

        private bool ContainsNullAttributes(VehicleDTO vehicle)
        {
            bool containsNull = false;

            containsNull = containsNull || vehicle.Brand == null;
            containsNull = containsNull || vehicle.Color == null;
            containsNull = containsNull || vehicle.Model == null;
            containsNull = containsNull || vehicle.Type == null;
            containsNull = containsNull || vehicle.Vin == null;

            return containsNull;
        }

        public void SetVehicleReadyToSell(string vin)
        {
            VehicleDTO vehicle = this.vehicleDAO.FindVehicleByVin(vin);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException("El vehiculo con vin: " + vin + " no esta registrado en el sistema.");
            }

            List<FlowItemDTO> flow = this.flowDAO.GetFlow();
            string lastStepOfFLowName = "";
            int lastStepNumber = 0;
            foreach(FlowItemDTO f in flow)
            {
                if(lastStepNumber < f.StepNumber)
                {
                    lastStepNumber = f.StepNumber;
                    lastStepOfFLowName = f.FlowStep.Name;
                }
            }

            ZoneDTO zone = this.zoneDAO.GetVehicleZone(vin);
            if(zone == null)
            {
                throw new ZoneNotFoundException("No se ha encontrado una subzona que contenga al vehiculo con vin: " + vin);
            }

            if(zone.FlowStep.Name == lastStepOfFLowName)
            {
                vehicle.Status = StatusCode.ReadyToSell;
                this.zoneDAO.RemoveVehicle(vin);
                this.vehicleDAO.UpdateVehicle(vehicle);
            } else
            {
                throw new FlowStepOrderException("El vehiculo no puede ser puesto a la venta, ya que no se encuentra en una subzona que se encuentre en el ultimo paso del flujo definido");
            }
        }

        public void SellVehicle(VehicleDTO vehicle)
        {
            if (vehicle.Price <= 0 || vehicle.Vin == null || vehicle.Vin == "")
            {
                throw new VehicleNullAttributesException("Se necesita un precio mayor a 0, y un vin para poder vender el vehiculo");
            }

            VehicleDTO realVehicle = this.vehicleDAO.FindVehicleByVin(vehicle.Vin);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException("El vehiculo con vin: " + vehicle.Vin + " no esta registrado en el sistema.");
            }


            if (realVehicle.Status == StatusCode.ReadyToSell)
            {
                realVehicle.Price = vehicle.Price;
                realVehicle.Status = StatusCode.Sold;

                this.vehicleDAO.UpdateVehicle(realVehicle);
            } else
            {
                throw new FlowStepOrderException("El vehiculo no se puede vender ya que no se encuentra listo para la venta.");
            }

        }
    }
}
