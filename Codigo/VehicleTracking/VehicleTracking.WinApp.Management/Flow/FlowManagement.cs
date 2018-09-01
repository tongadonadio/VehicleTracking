using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Services.Services;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.WinApp.Management.Flow;
using VehicleTracking.WinApp.Management.Observer;

namespace VehicleTracking.WinApp.Management
{
    public partial class FlowManagement : UserControl, WindowsObserver
    {

        FlowService flowService;

        public FlowManagement()
        {
            this.flowService = new FlowServiceImp(new FlowDAOImp());
            InitializeComponent();
            this.addBtn.FlatStyle = FlatStyle.Flat;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.newFlow.FlatStyle = FlatStyle.Flat;
            this.newFlow.FlatAppearance.BorderSize = 0;
            this.loadFlow();
            this.loadTypes();
        }

        private void loadFlow()
        {
            List<FlowItemDTO> flow = this.flowService.GetFlow();
            listFlow.Items.Clear();
            listFlow.Update();
            listFlow.Refresh();
            foreach (FlowItemDTO flowItemDTO in flow)
            {
                ListViewItem flowItem = new ListViewItem(flowItemDTO.StepNumber.ToString());
                flowItem.SubItems.Add(flowItemDTO.FlowStep.Name);

                listFlow.Items.Add(flowItem);
            }
        }

        private void loadTypes()
        {
            List<FlowStepDTO> flowSteps = this.flowService.GetAllSteps();
            listTypes.Items.Clear();
            listTypes.Update();
            listTypes.Refresh();
            foreach(FlowStepDTO flowStep in flowSteps)
            {
                ListViewItem typeItem = new ListViewItem(flowStep.Name);
                listTypes.Items.Add(typeItem);
            }
        }

        private void newFlow_Click(object sender, EventArgs e)
        {
            NewFlow newFlowWindows = new NewFlow(this, this.flowService, this.flowService.GetAllSteps());
            newFlowWindows.ShowDialog();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (txtType.Text != "")
            {
                FlowStepDTO flowStep = new FlowStepDTO(txtType.Text);

                try
                {
                    this.flowService.CreateStep(flowStep);
                    txtType.Text = "";
                    this.loadTypes();
                }
                catch (FlowStepAlreadyExistException)
                {
                    MessageBox.Show(
                        "Ya existe un tipo de zona creado con ese nombre",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show(
                        "No se puede crear un tipo de zona vacio",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateForm()
        {
            this.loadFlow();
        }
    }
}
