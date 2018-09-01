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
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Web.Models;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.WinApp.Management.Subject;

namespace VehicleTracking.WinApp.Management.Vehicle
{
    public partial class AddVehicles : Form
    {

        private WindowsSubject subject;

        private VehicleService vehicleService;

        public AddVehicles(WindowsObserver observer, VehicleService vehicleService)
        {
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.vehicleService = vehicleService;
            InitializeComponent();
            this.addBtn.FlatStyle = FlatStyle.Flat;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = FlatStyle.Flat;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (isCreationValid())
            {
                VehicleDTO newVehicle = new VehicleDTO();
                newVehicle.Vin = txtVin.Text;
                newVehicle.Model = txtModel.Text;
                newVehicle.Type = txtType.Text;
                newVehicle.Year = (int)numericYear.Value;
                newVehicle.Color = txtColor.Text;
                newVehicle.Brand = txtBrand.Text;

                try
                {
                    this.vehicleService.AddVehicle(newVehicle);
                    this.subject.Notify();
                    this.Close();
                }
                catch (VehicleNullAttributesException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (VehicleVinDuplicatedException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show(
                    "Los valores deben estar todos completos, y el año debe ser un numero entero.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isCreationValid()
        {
            bool isEmpty = false;

            isEmpty = isEmpty || txtBrand.Text == null || txtBrand.Text == "";
            isEmpty = isEmpty || txtColor.Text == null || txtColor.Text == "";
            isEmpty = isEmpty || txtModel.Text == null || txtModel.Text == "";
            isEmpty = isEmpty || txtType.Text == null || txtType.Text == "";
            isEmpty = isEmpty || txtVin.Text == null || txtVin.Text == "";
            isEmpty = isEmpty || (numericYear.Value % 1) != 0 || numericYear.Value <= 0;

            return !isEmpty;
        }
        
    }
}
