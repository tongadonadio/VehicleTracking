using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public interface FlowDAO
    {

        void CreateFlow(Dictionary<int, FlowStepDTO> steps);

        bool IsStepAvailable(FlowStepDTO step);

        void CreateStep(FlowStepDTO step);

        void DeleteStep(FlowStepDTO step);

        FlowStepDTO GetStepByName(string name);

        List<FlowStepDTO> GetAllSteps();

        List<FlowItemDTO> GetFlow();

    }
}
