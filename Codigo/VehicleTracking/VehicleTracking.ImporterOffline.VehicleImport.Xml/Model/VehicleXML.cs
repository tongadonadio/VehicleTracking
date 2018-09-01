using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleTracking.ImporterOffline.VehicleImport.Xml.Model
{
    public class VehicleXML
    {
        [XmlElement(ElementName = "Brand")]
        public string Brand { get; set; }
        [XmlElement(ElementName = "Model")]
        public string Model { get; set; }
        [XmlElement(ElementName = "Year")]
        public int Year { get; set; }
        [XmlElement(ElementName = "Color")]
        public string Color { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Vin")]
        public string Vin { get; set; }
        [XmlElement(ElementName = "CurrentLocation")]
        public string CurrentLocation { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
    }
}
