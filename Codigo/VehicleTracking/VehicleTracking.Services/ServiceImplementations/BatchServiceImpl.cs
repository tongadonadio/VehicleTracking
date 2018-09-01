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

namespace VehicleTracking.Services.ServiceImplementations
{
    public class BatchServiceImpl : BatchService
    {

        private UserDAO userDAO;
        private VehicleDAO vehicleDAO;
        private BatchDAO batchDAO;

        public BatchServiceImpl(BatchDAO batchDAO, UserDAO userDAO, VehicleDAO vehicleDAO)
        {
            this.userDAO = userDAO;
            this.batchDAO = batchDAO;
            this.vehicleDAO = vehicleDAO;
        }

        public Guid CreateBatch(BatchDTO batch)
        {
            Guid id = batch.Id;
            UserDTO user = userDAO.FindUserByUserName(batch.CreatorUserName);
            bool existUser = user != null;
            bool existVehicles = this.ExistVehicles(batch.Vehicles);
            bool batchContainsMoreThanOneVehicle = batch.Vehicles != null && batch.Vehicles.Count > 0;
            if (existVehicles && batchContainsMoreThanOneVehicle && existUser)
            {
                bool vehicleIsAssignedInOtherBatch = this.CheckedVehicle(batch.Vehicles);

                if (!vehicleIsAssignedInOtherBatch)
                {
                    id = batchDAO.AddBatch(batch);
                } else if (vehicleIsAssignedInOtherBatch)
                {
                    throw new VehicleInOtherBatchException("El vehículo está registrado en otro lote");
                }
            } else if (!batchContainsMoreThanOneVehicle)
            {
                throw new VehicleNotFoundException("No se han encontrado vehiculos en el lote que desea crear");
            } else if (!existVehicles)
            {
                throw new VehicleNotFoundException("Un vehículo ingresado no está registrado en el sistema.");
            } else if (!existUser)
            {
                throw new UserNotExistException("El usuario ingresado no está registrado en el sistema.");
            }

            return batch.Id;
        }

        public BatchDTO FindBatchById(Guid id)
        {
            return batchDAO.FindBatchById(id);
        }

        private bool CheckedVehicle(List<string> vehicles)
        {
            bool isAssigned = true;
            foreach (string vin in vehicles)
            {
                isAssigned = vehicleDAO.IsAssigned(vin) && isAssigned;
            }
            return isAssigned;
        }

        private bool ExistVehicles(List<string> vehicles)
        {
            bool exist = true;
            if (vehicles != null)
            {
                foreach (string vin in vehicles)
                {
                    exist = (vehicleDAO.FindVehicleByVin(vin) != null) && exist;
                }
            } else
            {
                exist = false;
            }

            return exist;
        }

        public List<BatchDTO> GetAllBatches()
        {
            return this.batchDAO.GetAllBatches();
        }
    }
}
