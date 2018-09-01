using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using VehicleTracking.ImporterOffline;
using VehicleTracking.ImporterOffline.VehicleImport.Xml.Model;
using VehicleTracking.Web.Models;

namespace VehicleTracking.ImporterOffline.VehicleImport.Xml
{
    public class VehicleImportOffline : IVehicleImportOffline
    {
        public string Id => "VehicleTracking.ImporterOffline.VehicleImport.Xml";

        public string Name => "XML Vehicle Importer";

        public string Version => "1.0";

        public void Initialize()
        {
        }

        public List<VehicleDTO> ImportVehicles()
        {
            var openFileDialog = new OpenFileDialog();
            var openFileDialogResult = openFileDialog.ShowDialog();

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
                var xmlSerializer = new XmlSerializer(typeof(VehicleListXML));

                using(var reader = new StringReader(fileContents))
                {
                    var vehiclesXML = (VehicleListXML)xmlSerializer.Deserialize(reader);

                    foreach (var vehicleXML in vehiclesXML.Items)
                    {
                        vehicles.Add(new VehicleDTO()
                        {
                            Id = Guid.NewGuid(),
                            Brand = vehicleXML.Brand,
                            Model = vehicleXML.Model,
                            Year = vehicleXML.Year,
                            Color = vehicleXML.Color,
                            Type = vehicleXML.Type,
                            Vin = vehicleXML.Vin,
                            CurrentLocation = vehicleXML.CurrentLocation,
                            Status = vehicleXML.Status
                        });
                    }
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
