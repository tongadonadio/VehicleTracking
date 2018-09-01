using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Services
{
    public interface FlowService
    {

        void CreateFlow(Dictionary<int, FlowStepDTO> steps);

        void CreateStep(FlowStepDTO step);

        FlowStepDTO GetStepByName(string name);

        List<FlowStepDTO> GetAllSteps();

        List<FlowItemDTO> GetFlow();

    }
}
