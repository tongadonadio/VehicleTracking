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
    public partial class ModifyZone : Form
    {

        private ZoneDTO zoneToModify;
        private WindowsSubject subject;
        private ZoneService zoneService;

        public ModifyZone(ZoneDTO zoneToModify, WindowsObserver observer, ZoneService zoneService)
        {
            this.zoneToModify = zoneToModify;
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.zoneService = zoneService;
            InitializeComponent();
            this.modifyBtn.FlatStyle = FlatStyle.Flat;
            this.modifyBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = FlatStyle.Flat;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.loadInfo();
        }

        private void loadInfo()
        {
            this.txtName.Text = this.zoneToModify.Name;
            this.numericCapacity.Value = this.zoneToModify.MaxCapacity;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (isModificationValid())
            {
                zoneToModify.Name = txtName.Text;
                zoneToModify.MaxCapacity = (int)numericCapacity.Value;

                this.zoneService.Update(zoneToModify);
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

        private bool isModificationValid()
        {
            bool isEmpty = false;

            isEmpty = isEmpty || txtName.Text == null || txtName.Text == "";
            isEmpty = isEmpty || (numericCapacity.Value % 1) != 0 || numericCapacity.Value <= 0;

            return !isEmpty;
        }
    }
}
