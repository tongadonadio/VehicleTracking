using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;
using WebTracking.Repository;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class TransportServiceImpl : TransportService
    {
        private TransportDAO transportDAO;
        private InspectionService inspectionService;
        private BatchService batchService;
        private VehicleService vehicleService;

        public TransportServiceImpl(TransportDAO transportDAO, InspectionService inspectionService,
                                    BatchService batchService, VehicleService vehicleService)
        {
            this.transportDAO = transportDAO;
            this.inspectionService = inspectionService;
            this.batchService = batchService;
            this.vehicleService = vehicleService;
        }

        public Guid CreateTransport(TransportDTO transport)
        {
            return transportDAO.AddTransport(transport);
        }

        public TransportDTO FindTransportById(Guid id)
        {
            return transportDAO.FindTransportById(id);
        }

        public List<TransportDTO> GetAllTransports()
        {
            return this.transportDAO.GetAllTransports();
        }

        public void StartTransport(List<Guid> batchesId, UserDTO user)
        {
            bool existBatches = ExistBatches(batchesId);
            if (existBatches)
            {
                bool batchesAreReady = BatchesAreReady(batchesId);
                if (batchesAreReady)
                {
                    List<BatchDTO> listBatches = new List<BatchDTO>();
                    foreach (Guid id in batchesId)
                    {
                        BatchDTO batch = batchService.FindBatchById(id);
                        foreach (string vin in batch.Vehicles)
                        {
                            VehicleDTO vehicleDTO = vehicleService.FindVehicleByVin(vin);
                            vehicleDTO.Status = StatusCode.InTransit;
                            vehicleService.UpdateVehicle(vehicleDTO);
                        }
                        listBatches.Add(batch);
                    }
                    TransportDTO transport = new TransportDTO();
                    transport.Batches = listBatches;
                    transport.StartDate = DateTime.Now;
                    transport.User = user;
                    this.CreateTransport(transport);
                } else
                {
                    throw new BatchIsNotReadyException("Hay vehículos en el lote que aún no se pueden transportar"); 
                }
            } else
            {
                throw new BatchNotFoundException("Un lote ingresado no está registrado en el sistema");
            }
        }

        public void FinishTransport(Guid id)
        {
            if (ExistTransport(id))
            {
                TransportDTO transport = this.transportDAO.FindTransportById(id);
                transport.EndDate = DateTime.Now;
                this.transportDAO.UpdateTransport(transport);
                foreach (BatchDTO batch in transport.Batches)
                {
                    foreach (string vin in batch.Vehicles)
                    {
                        VehicleDTO vehicle = this.vehicleService.FindVehicleByVin(vin);
                        vehicle.Status = StatusCode.Waiting;
                        this.vehicleService.UpdateVehicle(vehicle);
                    }
                }
            } else
            {
                throw new TransportNotFoundException("Un transporte ingresado no está registrado en el sistema");
            }
            
        }

        private bool BatchesAreReady(List<Guid>batchesId)
        {
            bool areReady = true;
            foreach (Guid id in batchesId)
            {
                BatchDTO batchDTO = this.batchService.FindBatchById(id);
                foreach (string vin in batchDTO.Vehicles)
                {
                    areReady = (this.inspectionService.ExistVehicleInspection(vin)) ? areReady : false;
                }
            }
            return areReady;
        }

        private bool ExistBatches(List<Guid> batchesId)
        {
            bool exist = true;
            foreach (var id in batchesId)
            {
                exist = (this.batchService.FindBatchById(id) != null) ? exist : false;
            }
            return exist;
        }

        private bool ExistTransport(Guid id)
        {
            return this.transportDAO.FindTransportById(id) != null;
        }
    }
}
