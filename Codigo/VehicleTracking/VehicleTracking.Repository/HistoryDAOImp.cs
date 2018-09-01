using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;
using WebTracking.Repository.Mappers;

namespace VehicleTracking.Repository
{
    public class HistoryDAOImp : HistoryDAO
    {
        public VehicleHistoryDTO GetHistory(string vin)
        {
            VehicleHistoryDTO historyDTO = null;
            List<HistoricVehicle> historicVehicles = null;
            List<Inspection> historicInspections = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                historicVehicles = context.HistoricVehicles
                    .Where(hv => hv.Vin == vin)
                    .ToList();

                historicInspections = context.Inspections
                    .Include("IdUser")
                    .Include("IdLocation")
                    .Include("Damages")
                    .Include("IdVehicle")
                    .Where(i => i.IdVehicle.Vin == vin)
                    .ToList();

                if((historicVehicles != null && historicVehicles.Count > 0) || (historicInspections != null && historicInspections.Count > 0))
                {
                    historyDTO = new VehicleHistoryDTO();

                    if (historicVehicles != null && historicVehicles.Count > 0)
                    {
                        List<HistoricVehicleDTO> historicVehiclesDTO = new List<HistoricVehicleDTO>();
                        foreach(HistoricVehicle historicVehicle in historicVehicles)
                        {
                            HistoricVehicleDTO historicVehicleDTO = new HistoricVehicleDTO();
                            historicVehicleDTO.Brand = historicVehicle.Brand;
                            historicVehicleDTO.Color = historicVehicle.Color;
                            historicVehicleDTO.CurrentLocation = historicVehicle.CurrentLocation;
                            historicVehicleDTO.Date = historicVehicle.Date;
                            historicVehicleDTO.Model = historicVehicle.Model;
                            historicVehicleDTO.Status = historicVehicle.Status;
                            historicVehicleDTO.Type = historicVehicle.Type;
                            historicVehicleDTO.Vin = historicVehicle.Vin;
                            historicVehicleDTO.Year = historicVehicle.Year;

                            historicVehiclesDTO.Add(historicVehicleDTO);
                        }
                        historyDTO.VehicleHistory = historicVehiclesDTO;
                    }

                    if (historicInspections != null && historicInspections.Count > 0)
                    {
                        List<InspectionDTO> historicInspectionsDTO = new List<InspectionDTO>();
                        InspectionMapper inspectionMapper = new InspectionMapper();

                        foreach(Inspection historicInspection in historicInspections)
                        {
                            InspectionDTO historicInspectionDTO = inspectionMapper.ToDTO(historicInspection);
                            historicInspectionsDTO.Add(historicInspectionDTO);
                        }

                        historyDTO.InspectionHistory = historicInspectionsDTO;
                    }
                    
                }
            }

            return historyDTO;
        }
    }
}
