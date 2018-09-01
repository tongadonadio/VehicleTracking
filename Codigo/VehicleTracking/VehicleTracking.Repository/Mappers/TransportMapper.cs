using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace VehicleTracking.Repository.Mappers
{
    public class TransportMapper
    {
        public TransportMapper() { }

        public TransportDTO ToDTO(Transport transport)
        {
            UserMapper userMapper = new UserMapper(new RoleDAOImp());
            BatchMapper batchMapper = new BatchMapper();

            TransportDTO transportDTO = new TransportDTO();
            transportDTO.Id = transport.Id;
            transportDTO.StartDate = transport.StartDate;
            transportDTO.EndDate = transport.EndDate;
            transportDTO.User = userMapper.ToDTO(transport.IdUser);

            List<BatchDTO> batchesDTO = new List<BatchDTO>();
            foreach (Batch batch in transport.Batches)
            {
                BatchDTO batchDTO = batchMapper.ToDTO(batch);
                batchesDTO.Add(batchDTO);
            }

            transportDTO.Batches = batchesDTO;

            return transportDTO;
        }

        public Transport ToEntity(TransportDTO transportDTO)
        {
            UserMapper userMapper = new UserMapper(new RoleDAOImp());
            BatchMapper batchMapper = new BatchMapper();

            Transport transport = new Transport();
            transport.Id = transportDTO.Id;
            transport.StartDate = transportDTO.StartDate;
            transport.EndDate = transportDTO.EndDate;
            transport.IdUser = userMapper.ToEntity(transportDTO.User);

            List<Batch> batches = new List<Batch>();
            foreach (BatchDTO batchDTO in transportDTO.Batches)
            {
                Batch batch = batchMapper.ToEntity(batchDTO);
                batches.Add(batch);
            }

            transport.Batches = batches;

            return transport;
        }
    }
}
