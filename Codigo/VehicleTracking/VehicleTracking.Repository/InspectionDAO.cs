using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace WebTracking.Repository
{
    public interface InspectionDAO
    {

        Guid AddInspection(InspectionDTO inspection);

        InspectionDTO FindInspectionById(Guid Id);

        List<InspectionDTO> GetAllInspections();

        bool ExistInspectionInPort(string vin);

        bool ExistVehicleInspection(string vin);
    }
}
