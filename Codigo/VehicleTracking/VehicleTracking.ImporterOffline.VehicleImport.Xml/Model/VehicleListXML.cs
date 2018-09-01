using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleTracking.ImporterOffline.VehicleImport.Xml.Model
{
    [XmlRoot("Vehicles")]
    public class VehicleListXML
    {
        [XmlElement("Vehicle")]
        public List<VehicleXML> Items { get; set; }

        public VehicleListXML()
        {
            this.Items = new List<VehicleXML>();
        }
    }
}
