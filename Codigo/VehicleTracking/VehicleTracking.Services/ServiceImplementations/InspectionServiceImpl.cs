using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;
using WebTracking.Repository;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class InspectionServiceImpl : InspectionService
    {
        private InspectionDAO inspectionDAO;
        private VehicleDAO vehicleDAO;

        public InspectionServiceImpl(InspectionDAO inspectionDAO, VehicleDAO vehicleDAO)
        {
            this.inspectionDAO = inspectionDAO;
            this.vehicleDAO = vehicleDAO;
        }

        public Guid CreateInspection(InspectionDTO inspection)
        {
            bool existVehicle = this.ExistVehicle(inspection.IdVehicle);
            bool damagesHaveImages = this.DamageHaveImage(inspection.Damages);
            if (existVehicle && damagesHaveImages)
            {
                string statusVehicle = this.StatusVehicle(inspection.IdVehicle);
                if (statusVehicle == StatusCode.InPort)
                {
                    this.CreateInspectionInPort(inspection);
                } else if (statusVehicle == StatusCode.Waiting)
                {
                    this.CreateInspectionInYard(inspection);
                } else
                {
                    throw new InspectionNotFoundException("Error al crear la inspección debido a que el vehículo no se encuentra en lugar de inspección");
                }
            } else if (!damagesHaveImages)
            {
                throw new ImageNotFoundException("Un daño que desea ingresar no contiene ninguna imagen asociada.");
            } else if (!existVehicle)
            {
                throw new VehicleNotFoundException("El vehículo ingresado no está registrado en el sistema.");
            }
            return inspection.Id;
        }

        private Guid CreateInspectionInPort(InspectionDTO inspection)
        {
            Guid id = Guid.NewGuid();
            if (inspection.Location == "Puerto 1" || inspection.Location == "Puerto 2" || inspection.Location == "Puerto 3")
            {
                inspectionDAO.AddInspection(inspection);
            }
            else
            {
                throw new InspectionNotFoundException("El lugar de inspección no coincide con el lugar del vehículo");
            }
            return id;
        }

        private Guid CreateInspectionInYard(InspectionDTO inspection)
        {
            Guid id = Guid.NewGuid();
            if (inspection.Location == "Patio 1" || inspection.Location == "Patio 2" || inspection.Location == "Patio 3")
            {
                inspectionDAO.AddInspection(inspection);
            }
            else
            {
                throw new InspectionNotFoundException("El lugar de inspección no coincide con el lugar del vehículo");
            }
            return id;
        }

        public InspectionDTO FindInspectionById(Guid id)
        {
            return this.inspectionDAO.FindInspectionById(id);
        }

        public List<InspectionDTO> GetAllInspections()
        {
            return this.inspectionDAO.GetAllInspections();
        }

        public bool ExistVehicleInspection(string vin)
        {
            return this.inspectionDAO.ExistVehicleInspection(vin);
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

        private string StatusVehicle(string vin)
        {
            VehicleDTO vehicle = vehicleDAO.FindVehicleByVin(vin);
            return vehicle.Status;
        }

        private bool ExistVehicle(string vin)
        {
            return vehicleDAO.FindVehicleByVin(vin) != null;
        }

        private bool DamageHaveImage(List<DamageDTO> damages)
        {
            bool have = true;
            if (damages != null)
            {
                foreach (DamageDTO damage in damages)
                {
                    if (damage.Images == null)
                    {
                        have = false;
                    }
                }
            }
            return have;
        }
    }
}
