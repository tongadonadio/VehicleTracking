using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository.Mappers;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public class TransportDAOImp : TransportDAO
    {
        private TransportMapper transportMapper;

        public TransportDAOImp()
        {
            this.transportMapper = new TransportMapper();
        }

        public Guid AddTransport(TransportDTO transportDTO)
        {
            Transport transport = transportMapper.ToEntity(transportDTO);
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                transport.IdUser = this.GetUser(context, transport.IdUser.UserName);
                transport.Batches = this.GetBatches(context, transport.Batches);

                context.Users.Attach(transport.IdUser);
                foreach (Batch batch in transport.Batches)
                {
                    context.Batches.Attach(batch);
                }

                context.Transports.Add(transport);
                context.SaveChanges();
            }

            return transport.Id;
        }

        public TransportDTO FindTransportById(Guid Id)
        {
            TransportDTO transportDTO = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Transport transport = context.Transports
                    .Include("IdUser")
                    .Include("Batches")
                    .Where(t => t.Id == Id)
                    .ToList().FirstOrDefault();
                if (transport != null)
                {
                    transportDTO = transportMapper.ToDTO(transport);
                }
                return transportDTO;
            }
        }


        public List<TransportDTO> GetAllTransports()
        {
            List<TransportDTO> transports = new List<TransportDTO>();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                List<Transport> listTransports = context.Transports
                    .Include("Batches")
                    .Include("IdUser")
                    .ToList();

                foreach (Transport transport in listTransports)
                {
                    TransportDTO transportDTO = this.transportMapper.ToDTO(transport);
                    transports.Add(transportDTO);
                }
            }
            return transports;
        }

        public void UpdateTransport(TransportDTO transportDTO)
        {
            Transport transport = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from t in context.Transports
                            where t.Id == transportDTO.Id
                            select t;
                transport = query.ToList().FirstOrDefault();

                if (transport != null)
                {
                    transport.EndDate = transportDTO.EndDate;
                    transport.StartDate = transportDTO.StartDate;
                    context.SaveChanges();
                }
            }
        }

        private User GetUser(VehicleTrackingDbContext context, string userName)
        {
            User user = context.Users
                    .Include("IdRole")
                    .Where(u => u.UserName == userName)
                    .ToList().FirstOrDefault();
            return user;
        }

        private List<Batch> GetBatches(VehicleTrackingDbContext context, List<Batch> batchesDTO)
        {
            List<Batch> batches = new List<Batch>();
            foreach (Batch batch in batchesDTO)
            {
                Batch resultbatch = new Batch();
                resultbatch = context.Batches
                    .Include("Vehicles")
                    .Include("IdUser")
                    .Where(b => b.Id == batch.Id)
                    .ToList().First();
                batches.Add(resultbatch);

                context.Batches.Attach(resultbatch);
            }
            return batches;
        }
    }
}
