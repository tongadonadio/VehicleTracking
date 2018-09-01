using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.ImporterOffline;
using VehicleTracking.ImporterOffline.VehicleImport.Json.Model;
using VehicleTracking.Web.Models;

namespace VehicleTracking.ImporterOffline.VehicleImport.Json
{
    public class VehicleImportOffline : IVehicleImportOffline
    {
        public string Id => "VehicleTracking.ImporterOffline.VehicleImport.Json";

        public string Name => "JSON Vehicle Importer";

        public string Version => "1.0";

        public void Initialize()
        {
        }

        public List<VehicleDTO> ImportVehicles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult openFileDialogResult = openFileDialog.ShowDialog();

            if (openFileDialogResult != null)
            {
                return ImportVehiclesFromFile(openFileDialog.FileName);
            }
            else
            {
                return new List<VehicleDTO>();
            }
        }

        public List<VehicleDTO> ImportVehiclesFromFile(string filePath)
        {
            List<VehicleDTO> vehicles = new List<VehicleDTO>();

            try
            {
                var fileContents = File.ReadAllText(filePath);
                var vehiclesJSON = JsonConvert.DeserializeObject<List<VehicleJSON>>(fileContents);

                foreach (var vehicleJSON in vehiclesJSON)
                {
                    vehicles.Add(new VehicleDTO()
                    {
                        Id = Guid.NewGuid(),
                        Brand = vehicleJSON.Brand,
                        Model = vehicleJSON.Model,
                        Year = vehicleJSON.Year,
                        Color = vehicleJSON.Color,
                        Type = vehicleJSON.Type,
                        Vin = vehicleJSON.Vin,
                        CurrentLocation = vehicleJSON.CurrentLocation,
                        Status = vehicleJSON.Status
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vehicles;
        }

    }
}
