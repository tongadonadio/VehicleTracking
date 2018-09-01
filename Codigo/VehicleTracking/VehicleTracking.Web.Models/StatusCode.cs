using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Web.Models
{
    public class StatusCode
    {
        public static readonly string InPort = "En puerto";
        public static readonly string InspectedInPort = "Inspeccionado en puerto";
        public static readonly string ReadyToGo = "Listo para partir en puerto";
        public static readonly string InTransit = "En transito";
        public static readonly string Waiting = "Esperando por inspeccion patio";
        public static readonly string ReadyToBeLocated = "Listo para ser ubicado en zona";
        public static readonly string Located = "Ubicado en zona";
        public static readonly string ReadyToSell = "Listo para la venta";
        public static readonly string Sold = "Vendido";

        public static List<string> GetAll()
        {
            List<string> allStatus = new List<string>(new string[] { InPort, ReadyToGo, InTransit, Waiting, Located });
            return allStatus;
        }

        public static bool IsValidStatus(string status)
        {
            List<string> allStatus = GetAll();
            return allStatus.Any(s => status.Equals(s));
        }
    }
}
