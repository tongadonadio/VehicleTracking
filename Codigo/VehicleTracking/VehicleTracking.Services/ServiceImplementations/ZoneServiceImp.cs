using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class ZoneServiceImp : ZoneService
    {

        private ZoneDAO zoneDAO;

        private FlowDAO flowDAO;

        private VehicleDAO vehicleDAO;

        public ZoneServiceImp(ZoneDAO zoneDAO, FlowDAO flowDAO, VehicleDAO vehicleDAO)
        {
            this.zoneDAO = zoneDAO;
            this.flowDAO = flowDAO;
            this.vehicleDAO = vehicleDAO;
        }

        public void AddZone(ZoneDTO zone)
        {
            bool isThereNullFields = this.IsThereNullFields(zone);
            if(isThereNullFields)
            {
                throw new ZoneNullAttributesException("Se debe ingresar el nombre de la zona, y la capacidad debe ser superior a 0");
            }

            if (zone.IsSubZone)
            {
                if (!this.flowDAO.IsStepAvailable(zone.FlowStep))
                {
                    this.zoneDAO.CreateZone(zone);
                } else
                {
                    throw new SubZoneWithoutFlowStepException("No se puede crear una subzona que no tiene un paso de flujo valido");
                }
            } else
            {
                this.zoneDAO.CreateZone(zone);
            }
        }

        public void AssignVehicle(Guid zoneId, string vin)
        {
            ZoneDTO zone = this.zoneDAO.FindZoneById(zoneId);
            int capacityLeft = this.zoneDAO.GetVehicleCapacityLeft(zoneId);

            if(zone.IsSubZone && capacityLeft > 0)
            {
                this.ValidateIsAllowedInFlow(zone, vin);
                this.zoneDAO.AssignVehicle(zoneId, vin);
            } else if(!zone.IsSubZone)
            {
                throw new AssignVehicleToMainZoneException("El vehiculo no puede ser ingresado en una zona principal, se debe ingresar en una subzona.");
            } else if(capacityLeft <= 0)
            {
                throw new AssignVehicleToZoneWithoutCapacityException("La subzona donde se desea ingresar el vehiculo no tiene mas capacidad");
            }
        }

        public void AssignZone(Guid from, Guid to)
        {
            ZoneDTO zone = this.zoneDAO.FindZoneById(to);
            ZoneDTO subZone = this.zoneDAO.FindZoneById(from);

            int capacityLeft = this.zoneDAO.GetZoneCapacityLeft(to);

            if(subZone.MaxCapacity <= capacityLeft && !zone.IsSubZone)
            {
                this.zoneDAO.AssignZone(from, to);
            } else if(zone.IsSubZone)
            {
                throw new ZoneCannotBeSubzoneOfAnotherSubzoneException("La zona a la que se le esta intentando ingresar una zona, ya es una subzona, por el momento solo existe 1 nivel de subzona en el sistema.");
            } else if(subZone.MaxCapacity > capacityLeft)
            {
                throw new ZoneOutOfCapacityException("La subzona que desea asignar tiene mayor capacidad que la capacidad restante de la zona principal, capacidad restante de la zona principal: " + capacityLeft);
            }
        }

        public ZoneDTO FindZoneById(Guid id)
        {
            ZoneDTO zone = this.zoneDAO.FindZoneById(id);

            if(zone == null)
            {
                throw new ZoneNotFoundException("La zona con id: " + id + " no se encuentra ingresada en el sistema");
            }

            return zone;
        }

        public List<ZoneDTO> GetAllZones()
        {
            return this.zoneDAO.GetAllZones();
        }

        public void RemoveVehicle(string vin)
        {
            this.zoneDAO.RemoveVehicle(vin);
        }

        private bool IsThereNullFields(ZoneDTO zone)
        {
            bool isThereNullFields = true;

            isThereNullFields = isThereNullFields && zone.Name == null;
            isThereNullFields = isThereNullFields && zone.MaxCapacity <= 0;

            return isThereNullFields;
        }

        private bool CheckMaxCapacity(ZoneDTO zone)
        {
            bool check = true;
            int maxCapacityZone = zone.MaxCapacity;
            int maxCapacitiySubZone = 0;
            foreach (ZoneDTO subZone in zone.SubZones)
            {
                if (subZone.MaxCapacity <= maxCapacityZone)
                {
                    maxCapacitiySubZone += subZone.MaxCapacity;
                    if (maxCapacitiySubZone > maxCapacityZone)
                    {
                        throw new ZoneOutOfCapacityException("La capacidad de la subZona supera la capacidad total de la zona");
                    }
                } else
                {
                    throw new ZoneOutOfCapacityException("La capacidad de la subZona supera la capacidad total de la zona");
                }
            }
            return check;
        }

        private void ValidateIsAllowedInFlow(ZoneDTO subZone, string vin)
        {
            VehicleDTO vehicle = this.vehicleDAO.FindVehicleByVin(vin);
            if(vehicle == null)
            {
                throw new VehicleNotFoundException("El vehiculo que se le intenta agregar una subzona no existe");
            }            

            List<FlowItemDTO> flow = this.flowDAO.GetFlow();
            ZoneDTO vehicleZone = this.zoneDAO.GetVehicleZone(vin);

            if(vehicleZone == null)
            {
                if (vehicle.Status != StatusCode.ReadyToBeLocated) {
                    throw new VehicleStatusDoesntAllowToAssignZoneException("El vehiculo no se encuentra en un estado correcto para ser asignado a una subzona");
                }

                FlowItemDTO firstStepOfFlow = flow.Find(f => f.StepNumber == 1); 
                if(subZone.FlowStep.Name != firstStepOfFlow.FlowStep.Name)
                {
                    throw new FlowStepOrderException("El vehiculo no puede ser asignado a esa subzona ya que no respeta el orden del flujo");
                }
            } else
            {
                if (vehicle.Status != StatusCode.Located)
                {
                    throw new VehicleStatusDoesntAllowToAssignZoneException("El vehiculo no se encuentra en un estado correcto para ser asignado a una subzona");
                }
                FlowItemDTO nextStepOfFlow = flow.Find(f => f.FlowStep.Name == subZone.FlowStep.Name);
                if(nextStepOfFlow == null)
                {
                    throw new SubezoneDoesNotBelongeToZoneException("La subzona a asignar no esta incluida en el flujo");
                } else
                {
                    FlowItemDTO currentStepOfFlow = flow.Find(f => f.FlowStep.Name == vehicleZone.FlowStep.Name);
                    if(currentStepOfFlow == null)
                    {
                        throw new SubezoneDoesNotBelongeToZoneException("La zona actual en la que se encuentra el vehiculo no pertenece al flujo");
                    } else
                    {
                        if(nextStepOfFlow.StepNumber != currentStepOfFlow.StepNumber+1)
                        {
                            throw new FlowStepOrderException("El vehiculo no puede ser asignado a esa subzona ya que no respeta el orden del flujo");
                        }
                    }
                }
            }
        }

        public void Update(ZoneDTO zone)
        {
            this.zoneDAO.Update(zone);
        }

        public void Delete(Guid id)
        {
            ZoneDTO zoneToDelete = this.zoneDAO.FindZoneById(id);
            
            if(zoneToDelete == null)
            {
                throw new ZoneNotFoundException("No se ha encontrado una zona con ese id");
            } else
            {
                if(zoneToDelete.SubZones != null && zoneToDelete.SubZones.Count > 0)
                {
                    throw new ZoneCannotBeDeletedException("La zona contiene subzonas y no puede ser borrada");
                }
                if (zoneToDelete.Vehicles != null && zoneToDelete.Vehicles.Count > 0)
                {
                    throw new ZoneCannotBeDeletedException("La zona contiene vehiculos y no puede ser borrada");
                }
                this.zoneDAO.Delete(id);
            }
            
        }

        public void AddSubZone(Guid id, ZoneDTO zone)
        {
            bool isThereNullFields = this.IsThereNullFields(zone);
            if (isThereNullFields)
            {
                throw new ZoneNullAttributesException("Se debe ingresar el nombre de la zona, y la capacidad debe ser superior a 0");
            }

            ZoneDTO mainZone = this.zoneDAO.FindZoneById(id);

            if (mainZone == null)
            {
                throw new ZoneNotFoundException("No se ha encontrado una zona con ese id");
            }

            zone.IsSubZone = true;

            if (!this.flowDAO.IsStepAvailable(zone.FlowStep))
            {
                this.zoneDAO.CreateSubZone(id, zone);
            }
            else
            {
                throw new SubZoneWithoutFlowStepException("No se puede crear una subzona que no tiene un paso de flujo valido");
            }
            
        }
    }
}
