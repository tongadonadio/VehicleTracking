using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace WebTracking.Repository
{
    public class InspectionDAOImp : InspectionDAO
    {
        private InspectionMapper inspectionMapper;
        private VehicleDAO vehicleDAO;

        public InspectionDAOImp(VehicleDAO vehicleDAO)
        {
            inspectionMapper = new InspectionMapper();
            this.vehicleDAO = vehicleDAO;
        }

        public Guid AddInspection(InspectionDTO inspection)
        {
            Inspection inspectionEntity = inspectionMapper.ToEntity(inspection);

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                inspectionEntity.IdUser = this.GetUser(context,inspection.CreatorUserName);
                Vehicle vehicle = this.GetVehicle(context, inspection.IdVehicle);
                inspectionEntity.IdVehicle = vehicle;
                inspectionEntity.IdLocation = this.GetLocation(context, inspection.Location);
                inspectionEntity.Damages = this.GetDamages(context, inspection.Damages);
                inspectionEntity.Date = DateTime.Now;
                
                if(vehicle.Status == StatusCode.Waiting)
                {
                    vehicle.Status = StatusCode.ReadyToBeLocated;
                    var entry = context.Entry(vehicle);
                    entry.Property(sz => sz.Status).IsModified = true;

                    HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(vehicle);
                    context.HistoricVehicles.Add(historicVehicle);
                } else if(vehicle.Status == StatusCode.InPort)
                {
                    vehicle.Status = StatusCode.InspectedInPort;
                    var entry = context.Entry(vehicle);
                    entry.Property(sz => sz.Status).IsModified = true;

                    HistoricVehicle historicVehicle = mapVehicleToHistoricVehicle(vehicle);
                    context.HistoricVehicles.Add(historicVehicle);                    
                }
                

                context.Users.Attach(inspectionEntity.IdUser);
                
                context.Inspections.Add(inspectionEntity);
                context.SaveChanges();
            }

            return inspectionEntity.Id;
        }

        public List<InspectionDTO> GetAllInspections()
        {
            List<InspectionDTO> inspections = new List<InspectionDTO>();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                List<Inspection> inspectionsEntities = context.Inspections
                    .Include("Damages")
                    .Include("IdLocation")
                    .Include("IdUser")
                    .Include("IdVehicle")
                    .ToList();
                foreach (Inspection inspection in inspectionsEntities)
                {
                    InspectionDTO inspectionDTO = this.inspectionMapper.ToDTO(inspection);
                    inspections.Add(inspectionDTO);
                }
            }
            return inspections;
        }

        public InspectionDTO FindInspectionById(Guid id)
        {
            Inspection inspection = null;
            InspectionDTO inspectionDTO = null;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                inspection = context.Inspections
                    .Include("Damages")
                    .Include("IdLocation")
                    .Include("IdUser")
                    .Include("IdVehicle")
                    .Where(i => i.Id == id)
                    .ToList().FirstOrDefault();
                if (inspection != null)
                {
                    List<Damage> damages = new List<Damage>();
                    foreach (Damage damage in inspection.Damages)
                    {
                        Damage dam = context.Damages
                            .Include("Images")
                            .Where(d => d.Id == damage.Id)
                            .ToList().FirstOrDefault();
                        damages.Add(dam);
                    }
                    inspection.Damages = damages;
                }
            }
            if ( inspection != null)
            {
                inspectionDTO = this.inspectionMapper.ToDTO(inspection);
            }
            return inspectionDTO;
        }

        public bool ExistVehicleInspection(string vin)
        {
            bool exist = true;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                exist = context.Inspections.Any(i => i.IdVehicle.Vin == vin);
            }
            return exist;
        }

        private HistoricVehicle mapVehicleToHistoricVehicle(Vehicle vehicle)
        {
            HistoricVehicle historicVehicle = new HistoricVehicle();

            historicVehicle.Id = Guid.NewGuid();
            historicVehicle.Brand = vehicle.Brand;
            historicVehicle.Color = vehicle.Color;
            historicVehicle.CurrentLocation = vehicle.CurrentLocation;
            historicVehicle.Date = DateTime.Now;
            historicVehicle.Model = vehicle.Model;
            historicVehicle.Status = vehicle.Status;
            historicVehicle.Type = vehicle.Type;
            historicVehicle.Vin = vehicle.Vin;
            historicVehicle.Year = vehicle.Year;

            return historicVehicle;
        }

        public bool ExistInspectionInPort(string vin)
        {
            bool exist = false;
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                Inspection inspection = context.Inspections
                    .Include("IdLocation")
                    .Where(i => i.IdVehicle.Vin == vin)
                    .ToList().FirstOrDefault();

                if (inspection != null)
                {
                    exist = inspection.IdLocation.Name == Ports.FIRST_PORT.GetName() || exist;
                    exist = inspection.IdLocation.Name == Ports.SECOND_PORT.GetName() || exist;
                    exist = inspection.IdLocation.Name == Ports.THIRD_PORT.GetName() || exist;
                }
            }
            return exist;
        }

        private Batch GetBatchOfVehicle(VehicleTrackingDbContext context, Vehicle vehicle)
        {
            Batch batch = context.Batches
                    .Include("Vehicles")
                    .Where(b => b.Vehicles.Select(v => v.Id).Contains(vehicle.Id))
                    .ToList().FirstOrDefault();
            return batch;
        }

        private User GetUser(VehicleTrackingDbContext context, string userName)
        {
            User user = context.Users
                    .Include("IdRole")
                    .Where(u => u.UserName == userName)
                    .ToList().FirstOrDefault();
            return user;
        }

        private Vehicle GetVehicle(VehicleTrackingDbContext context, string vin)
        {
            Vehicle vehicle = context.Vehicles
                    .Where(v => v.Vin == vin)
                    .ToList().FirstOrDefault();
            return vehicle;
        }

        private Location GetLocation(VehicleTrackingDbContext context, string name)
        {
            Location location = context.Locations
                    .Where(l => l.Name == name)
                    .ToList().FirstOrDefault();
            if (location == null)
            {
                location = new Location();
                location.Id = Guid.NewGuid();
                location.Name = name;
            }
            else
            {
                context.Locations.Attach(location);
            }
            return location;
        }

        private List<Damage> GetDamages(VehicleTrackingDbContext context, List<DamageDTO> damages)
        {
            List<Damage> resultDamages = null;
            if (damages != null)
            {
                resultDamages = new List<Damage>();
                foreach (var damage in damages)
                {
                    Damage newDamage = new Damage();
                    newDamage.Description = damage.Description;
                    newDamage.Images = new List<Base64Image>();
                    foreach (Base64ImageDTO image in damage.Images)
                    {
                        Base64Image newImage = new Base64Image();
                        newImage.Base64EncodedImage = image.Base64EncodedImage;
                        newDamage.Images.Add(newImage);
                    }
                    resultDamages.Add(newDamage);
                }
            }
            return resultDamages;
        }
    }
}
