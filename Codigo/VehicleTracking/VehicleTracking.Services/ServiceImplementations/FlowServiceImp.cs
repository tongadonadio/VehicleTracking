using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.ServiceImplementations
{
    public class FlowServiceImp : FlowService
    {

        private FlowDAO flowDAO;

        public FlowServiceImp(FlowDAO flowDAO)
        {
            this.flowDAO = flowDAO;
        }

        public void CreateFlow(Dictionary<int, FlowStepDTO> steps)
        {
            this.validateRequest(steps);
            this.flowDAO.CreateFlow(steps);
        }

        public void CreateStep(FlowStepDTO step)
        {
            if(this.flowDAO.IsStepAvailable(step))
            {
                this.flowDAO.CreateStep(step);
            } else
            {
                throw new FlowStepAlreadyExistException("Ya existe un paso de flujo con ese nombre");
            }
        }

        public List<FlowStepDTO> GetAllSteps()
        {
            return this.flowDAO.GetAllSteps();
        }

        public List<FlowItemDTO> GetFlow()
        {
            return this.flowDAO.GetFlow();
        }

        public FlowStepDTO GetStepByName(string name)
        {
            FlowStepDTO flowStep = this.flowDAO.GetStepByName(name);

            if(flowStep == null)
            {
                throw new FlowStepNotFoundException("No se ha encontrado paso de flujo con ese nombre");
            }

            return flowStep;
        }

        void validateRequest(Dictionary<int, FlowStepDTO> request)
        {
            int numberOfSteps = request.Count;
            int startStep = 1;

            if(request.Values.GroupBy(s => s.Name).Any(r => r.Count() > 1))
            {
                throw new IncorrectCreateFlowRequestException("La solicitud de creacion de flujo esta mal armada");
            }

            for(int i=startStep; i<= numberOfSteps;i++)
            {
                if(!request.ContainsKey(i))
                {
                    throw new IncorrectCreateFlowRequestException("La solicitud de creacion de flujo esta mal armada");
                }
            }
        }
    }
}
