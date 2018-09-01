using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using VehicleTracking.Web.Models;
using VehicleTracking.Repository;
using VehicleTracking.Services.Services;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Exceptions;

namespace VehicleTracking.Tests.Flow
{
    [TestClass]
    public class FlowServiceTest
    {
        [TestMethod]
        public void CreateFlowSuccessfullyTest()
        {
            Dictionary<int, FlowStepDTO> request = new Dictionary<int, FlowStepDTO>();

            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO pintura = new FlowStepDTO("Pintura");
            FlowStepDTO lavado = new FlowStepDTO("Lavado");

            request.Add(1, mecanica);
            request.Add(2, pintura);
            request.Add(3, lavado);
            
            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.CreateFlow(request)).Verifiable();
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            flowService.CreateFlow(request);
        }

        [TestMethod]
        public void CreateStepSuccessfullyTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.IsStepAvailable(mecanica)).Returns(true);
            mockFlowDAO.Setup(f => f.CreateStep(mecanica)).Verifiable();
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            flowService.CreateStep(mecanica);
        }

        [TestMethod]
        [ExpectedException(typeof(FlowStepAlreadyExistException))]
        public void CreateStepErrorDuplicatedStepTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.IsStepAvailable(mecanica)).Returns(false);
            mockFlowDAO.Setup(f => f.CreateStep(mecanica)).Verifiable();
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            flowService.CreateStep(mecanica);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectCreateFlowRequestException))]
        public void ErrorCreatingFlowIncorrectNumbersTest()
        {
            Dictionary<int, FlowStepDTO> request = new Dictionary<int, FlowStepDTO>();

            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO pintura = new FlowStepDTO("Pintura");
            FlowStepDTO lavado = new FlowStepDTO("Lavado");

            request.Add(1, mecanica);
            request.Add(4, pintura);
            request.Add(3, lavado);

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.CreateFlow(request)).Verifiable();
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            flowService.CreateFlow(request);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectCreateFlowRequestException))]
        public void ErrorCreatingFlowIncorrectStepsTest()
        {
            Dictionary<int, FlowStepDTO> request = new Dictionary<int, FlowStepDTO>();

            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO pintura = new FlowStepDTO("Pintura");
            FlowStepDTO lavado = new FlowStepDTO("Lavado");

            request.Add(1, mecanica);
            request.Add(2, pintura);
            request.Add(3, pintura);

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.CreateFlow(request)).Verifiable();
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            flowService.CreateFlow(request);
        }

        [TestMethod]
        public void GetAllStepsTest()
        {
            List<FlowStepDTO> steps = new List<FlowStepDTO>();
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            steps.Add(mecanica);
            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.GetAllSteps()).Returns(steps);
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            List<FlowStepDTO> result = flowService.GetAllSteps();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetFlowTest()
        {
            List<FlowItemDTO> flowItems = new List<FlowItemDTO>();
            FlowItemDTO flowItem = new FlowItemDTO();
            flowItem.Id = Guid.NewGuid();
            flowItem.StepNumber = 1;
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            flowItem.FlowStep = mecanica;
            flowItems.Add(flowItem);

            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.GetFlow()).Returns(flowItems);
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            List<FlowItemDTO> result = flowService.GetFlow();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [ExpectedException(typeof(FlowStepNotFoundException))]
        [TestMethod]
        public void ErrorGettingStepByNameTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            FlowStepDTO noneStep = null;
            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.GetStepByName(mecanica.Name)).Returns(noneStep);
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            FlowStepDTO result = flowService.GetStepByName(mecanica.Name);
            Assert.AreEqual(mecanica.Name, result.Name);
        }

        [TestMethod]
        public void GetStepByNameTest()
        {
            FlowStepDTO mecanica = new FlowStepDTO("Mecanica ligera");
            var mockFlowDAO = new Mock<FlowDAO>();
            mockFlowDAO.Setup(f => f.GetStepByName(mecanica.Name)).Returns(mecanica);
            FlowService flowService = new FlowServiceImp(mockFlowDAO.Object);
            FlowStepDTO result = flowService.GetStepByName(mecanica.Name);
            Assert.AreEqual(mecanica.Name, result.Name);
        }
    }
}
