using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Services;
using VehicleTracking.Web.Models;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.WinApp.Management.Subject;

namespace VehicleTracking.WinApp.Management.Vehicle
{
    public partial class ModifyVehicles : Form
    {

        private WindowsSubject subject;
        private VehicleDTO vehicleToModify;
        private VehicleService vehicleService;

        public ModifyVehicles(VehicleDTO vehicleToModify,WindowsObserver observer, VehicleService vehicleService)
        {
            this.vehicleToModify = vehicleToModify;
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.vehicleService = vehicleService;
            InitializeComponent();
            this.modifyBtn.FlatStyle = FlatStyle.Flat;
            this.modifyBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = FlatStyle.Flat;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.loadData();
        }

        private void loadData()
        {
            txtModel.Text = vehicleToModify.Model;
            txtType.Text = vehicleToModify.Type;
            numericYear.Value = vehicleToModify.Year;
            txtColor.Text = vehicleToModify.Color;
            txtBrand.Text = vehicleToModify.Brand;
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (isModificationValid())
            {
                vehicleToModify.Model = txtModel.Text;
                vehicleToModify.Type = txtType.Text;
                vehicleToModify.Year = (int)numericYear.Value;
                vehicleToModify.Color = txtColor.Text;
                vehicleToModify.Brand = txtBrand.Text;

                this.vehicleService.UpdateVehicle(vehicleToModify);
                this.subject.Notify();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Los valores deben estar todos completos, y el año debe ser un numero entero.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool isModificationValid()
        {
            bool isEmpty = false;

            isEmpty = isEmpty || txtBrand.Text == null || txtBrand.Text == "";
            isEmpty = isEmpty || txtColor.Text == null || txtColor.Text == "";
            isEmpty = isEmpty || txtModel.Text == null || txtModel.Text == "";
            isEmpty = isEmpty || txtType.Text == null || txtType.Text == "";
            isEmpty = isEmpty || (numericYear.Value % 1) != 0 || numericYear.Value <= 0;

            return !isEmpty;
        }

    }
}
