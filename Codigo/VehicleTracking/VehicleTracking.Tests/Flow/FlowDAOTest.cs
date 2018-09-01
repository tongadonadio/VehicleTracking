using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository;
using System.Collections.Generic;

namespace VehicleTracking.Tests.Flow
{
    [TestClass]
    public class FlowDAOTest
    {
        [TestMethod]
        public void CreateStepTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowDAO flowDAO = new FlowDAOImp();

            if(flowDAO.IsStepAvailable(mecanica))
            {
                flowDAO.CreateStep(mecanica);
            }

            FlowStepDTO result = flowDAO.GetStepByName("Mecanica ligera");

            Assert.AreEqual(mecanica.Name, result.Name);
        }

        [TestMethod]
        public void GetStepByNameTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowDAO flowDAO = new FlowDAOImp();

            if (flowDAO.IsStepAvailable(mecanica))
            {
                flowDAO.CreateStep(mecanica);
            }

            FlowStepDTO result = flowDAO.GetStepByName("Mecanica ligera");

            Assert.AreEqual(mecanica.Name, result.Name);
        }

        [TestMethod]
        public void GetAllStepsTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowDAO flowDAO = new FlowDAOImp();

            if (flowDAO.IsStepAvailable(mecanica))
            {
                flowDAO.CreateStep(mecanica);
            }

            List<FlowStepDTO> results = flowDAO.GetAllSteps();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CreateFlowTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO pintura = new FlowStepDTO("Pintura");
            FlowStepDTO lavado = new FlowStepDTO("Lavado");
            FlowDAO flowDAO = new FlowDAOImp();

            if (flowDAO.IsStepAvailable(mecanica))
            {
                flowDAO.CreateStep(mecanica);
            }
            if (flowDAO.IsStepAvailable(pintura))
            {
                flowDAO.CreateStep(pintura);
            }
            if (flowDAO.IsStepAvailable(lavado))
            {
                flowDAO.CreateStep(lavado);
            }

            Dictionary<int, FlowStepDTO> request = new Dictionary<int, FlowStepDTO>();
            request.Add(1, mecanica);
            request.Add(2, pintura);
            request.Add(3, lavado);

            flowDAO.CreateFlow(request);
            List<FlowItemDTO> flowItems = flowDAO.GetFlow();
            Assert.IsNotNull(flowItems);
            Assert.IsTrue(flowItems.Count > 0);
            FlowItemDTO resultMecanica = flowItems.Find(f => f.FlowStep.Name == mecanica.Name);
            Assert.AreEqual(1, resultMecanica.StepNumber);
            FlowItemDTO resultPintura = flowItems.Find(f => f.FlowStep.Name == pintura.Name);
            Assert.AreEqual(2, resultPintura.StepNumber);
            FlowItemDTO resultLavado = flowItems.Find(f => f.FlowStep.Name == lavado.Name);
            Assert.AreEqual(3, resultLavado.StepNumber);
        }

    }
}
