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

namespace VehicleTracking.WinApp.Management.Zone
{
    public partial class AddZone : Form
    {

        private ZoneService zoneService;
        private FlowService flowService;
        private WindowsSubject subject;

        public AddZone(ZoneService zoneService, FlowService flowService, WindowsObserver observer)
        {
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.zoneService = zoneService;
            this.flowService = flowService;
            InitializeComponent();
            this.addBtn.FlatStyle = FlatStyle.Flat;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = FlatStyle.Flat;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.comboTypes.Enabled = false;
            this.init();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void init()
        {
            List<FlowStepDTO> flowSteps = this.flowService.GetAllSteps();
            foreach (FlowStepDTO flowStep in flowSteps) {
                this.comboTypes.Items.Add(flowStep.Name);
            }
            this.comboTypes.SelectedIndex = 0;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (isCreationValid())
            {
                ZoneDTO newZone = new ZoneDTO();
                newZone.Name = txtName.Text;
                newZone.IsSubZone = isSubZoneChk.Checked;
                newZone.MaxCapacity = (int)numericCapacity.Value;
                if (newZone.IsSubZone)
                {
                    FlowStepDTO flowStep = new FlowStepDTO(this.comboTypes.SelectedItem.ToString());
                    newZone.FlowStep = flowStep;
                }

                this.zoneService.AddZone(newZone);
                this.subject.Notify();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Los valores deben estar todos completos, y la capacidad debe ser mayor a 1.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isCreationValid()
        {
            bool isEmpty = false;

            isEmpty = isEmpty || txtName.Text == null || txtName.Text == "";
            isEmpty = isEmpty || (numericCapacity.Value % 1) != 0 || numericCapacity.Value <= 0;

            return !isEmpty;
        }

        private void isSubZoneChk_CheckedChanged(object sender, EventArgs e)
        {
            if(!this.isSubZoneChk.Checked)
            {
                this.comboTypes.Enabled = false;
            } else
            {
                this.comboTypes.Enabled = true;
            }
        }
    }
}
