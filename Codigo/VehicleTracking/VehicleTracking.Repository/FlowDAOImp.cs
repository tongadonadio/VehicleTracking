using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.DataAccess;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Repository
{
    public class FlowDAOImp : FlowDAO
    {
        public void CreateFlow(Dictionary<int, FlowStepDTO> steps)
        {
            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                foreach (FlowItem flowItem in context.FlowItems)
                {
                    context.FlowItems.Remove(flowItem);
                }
                
                foreach (int key in steps.Keys)
                {
                    FlowItem flowItem = new FlowItem();
                    flowItem.StepNumber = key;
                    flowItem.Id = Guid.NewGuid();
                    string stepName = steps[key].Name;
                    var query = from f in context.FlowSteps
                                where f.Name == stepName
                                select f;
                    FlowStep flowStep = query.ToList().FirstOrDefault();

                    flowItem.FlowStep = flowStep;

                    context.FlowItems.Add(flowItem);
                }
                context.SaveChanges();
            }
        }

        public void CreateStep(FlowStepDTO step)
        {
            FlowStep flowStep = new FlowStep();
            flowStep.Name = step.Name;
            flowStep.Id = Guid.NewGuid();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                context.FlowSteps.Add(flowStep);
                context.SaveChanges();
            }
        }

        public void DeleteStep(FlowStepDTO step)
        {
            FlowStep flowStep = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from f in context.FlowSteps
                            where f.Name == step.Name
                            select f;
                flowStep = query.ToList().FirstOrDefault();

                if (flowStep != null)
                {
                    context.FlowSteps.Remove(flowStep);
                    context.SaveChanges();
                }
            }
        }

        public List<FlowStepDTO> GetAllSteps()
        {
            List<FlowStepDTO> flowStepsDTO = new List<FlowStepDTO>();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                foreach(FlowStep flowStep in context.FlowSteps)
                {
                    FlowStepDTO flowStepDTO = new FlowStepDTO(flowStep.Name);
                    flowStepDTO.Id = flowStep.Id;
                    flowStepsDTO.Add(flowStepDTO);
                }
            }

            return flowStepsDTO;
        }

        public List<FlowItemDTO> GetFlow()
        {
            List<FlowItemDTO> flow = new List<FlowItemDTO>();

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                List<FlowItem> flowItems = context.FlowItems.Include("FlowStep").OrderBy(x => x.StepNumber).ToList();
                foreach (FlowItem flowItem in flowItems)
                {
                    FlowItemDTO flowItemDTO = new FlowItemDTO();
                    flowItemDTO.Id = flowItem.Id;
                    flowItemDTO.StepNumber = flowItem.StepNumber;
                    flowItemDTO.FlowStep = new FlowStepDTO(flowItem.FlowStep.Name);
                    flowItemDTO.FlowStep.Id = flowItem.FlowStep.Id;

                    flow.Add(flowItemDTO);
                }
            }

            return flow;
        }

        public FlowStepDTO GetStepByName(string name)
        {
            FlowStepDTO flowStepDTO = null;
            FlowStep flowStep = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from f in context.FlowSteps
                            where f.Name == name
                            select f;
                flowStep = query.ToList().FirstOrDefault();

                if (flowStep != null)
                {
                    flowStepDTO = new FlowStepDTO(name);
                }
            }

            return flowStepDTO;
        }

        public bool IsStepAvailable(FlowStepDTO step)
        {
            bool stepAvailable = false;
            FlowStep flowStep = null;

            using (VehicleTrackingDbContext context = new VehicleTrackingDbContext())
            {
                var query = from f in context.FlowSteps
                            where f.Name == step.Name
                            select f;
                flowStep = query.ToList().FirstOrDefault();

                if (flowStep == null)
                {
                    stepAvailable = true;
                }
            }

            return stepAvailable;
        }
    }
}
