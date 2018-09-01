using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.WinApp.Management.Subject;

namespace VehicleTracking.WinApp.Management.Flow
{
    public partial class NewFlow : Form
    {

        private WindowsSubject subject;

        private FlowService flowService;

        Dictionary<int,FlowStepDTO> flow;

        public NewFlow(WindowsObserver observer, FlowService flowService, List<FlowStepDTO> subzoneTypes)
        {
            flow = new Dictionary<int, FlowStepDTO>();
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.flowService = flowService;
            InitializeComponent();
            this.addType.FlatStyle = FlatStyle.Flat;
            this.addType.FlatAppearance.BorderSize = 0;
            this.removeType.FlatStyle = FlatStyle.Flat;
            this.removeType.FlatAppearance.BorderSize = 0;
            this.newFlowBtn.FlatStyle = FlatStyle.Flat;
            this.newFlowBtn.FlatAppearance.BorderSize = 0;
            this.loadTypes(subzoneTypes);
        }

        private void loadTypes(List<FlowStepDTO> flowSteps)
        {
            listTypes.Items.Clear();
            listTypes.Update();
            listTypes.Refresh();
            foreach (FlowStepDTO flowStep in flowSteps)
            {
                ListViewItem typeItem = new ListViewItem(flowStep.Name);
                listTypes.Items.Add(typeItem);
            }
        }

        private void addType_Click(object sender, EventArgs e)
        {
            if (this.listTypes.SelectedItems.Count > 0)
            {
                FlowStepDTO flowStep = new FlowStepDTO(this.listTypes.SelectedItems[0].Text);
                this.flow[this.flow.Count+1] = flowStep;
                this.removeSelectedType();
                this.refreshFlow();
            }
            else
            {
                MessageBox.Show(
                    "Seleccione un tipo de la lista.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshFlow()
        {
            listFlow.Items.Clear();
            listFlow.Update();
            listFlow.Refresh();
            foreach (int key in flow.Keys)
            {
                ListViewItem flowItem = new ListViewItem(key.ToString());
                flowItem.SubItems.Add(flow[key].Name);

                listFlow.Items.Add(flowItem);
            }
        }

        private void removeType_Click(object sender, EventArgs e)
        {
            if (this.listFlow.SelectedItems.Count > 0)
            {
                string removedType = this.listFlow.SelectedItems[0].SubItems[1].Text;
                this.flow.Remove(this.listFlow.Items.IndexOf(this.listFlow.SelectedItems[0]) + 1);
                int reindex = 1;
                Dictionary<int, FlowStepDTO> changedFlow = new Dictionary<int, FlowStepDTO>();
                foreach (int key in this.flow.Keys)
                {
                    FlowStepDTO tempFlow = this.flow[key];
                    changedFlow[reindex] = tempFlow;
                    reindex++;
                }
                this.addSelectedType(removedType);
                this.flow = changedFlow;
                this.refreshFlow();
            }
            else
            {
                MessageBox.Show(
                    "Seleccione un paso del flujo de la lista.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeSelectedType()
        {
            this.listTypes.Items.RemoveAt(this.listTypes.Items.IndexOf(this.listTypes.SelectedItems[0]));
        }

        private void addSelectedType(string name)
        {
            ListViewItem flowItem = new ListViewItem(name);
            listTypes.Items.Add(flowItem);
            listTypes.Update();
            listTypes.Refresh();
        }

        private void newFlowBtn_Click(object sender, EventArgs e)
        {
            if (this.flow.Count > 0)
            {
                this.flowService.CreateFlow(this.flow);
                this.subject.Notify();
                this.Close();
            } else
            {
                MessageBox.Show(
                    "No hay ningun tipo de subzona en el flujo.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
