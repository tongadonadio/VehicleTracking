using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Web.Models;

namespace VehicleTracking.WinApp.Management.Zone
{
    public partial class ZoneDetails : Form
    {

        private ZoneDTO zone;

        public ZoneDetails(ZoneDTO zone)
        {
            this.zone = zone;
            InitializeComponent();
            this.init();
        }

        private void init()
        {
            this.idLbl.Text = this.idLbl.Text + " " + this.zone.Id.ToString();
            this.nameLbl.Text = this.nameLbl.Text + " " + this.zone.Name;
            this.isSubZoneLbl.Text = this.isSubZoneLbl.Text + " " + (this.zone.IsSubZone ? "Si" : "No");
            this.capacityLbl.Text = this.capacityLbl.Text + " " + this.zone.MaxCapacity.ToString();
            this.typeLbl.Text = this.typeLbl.Text + " " + (this.zone.FlowStep == null ? "Sin tipo" : this.zone.FlowStep.Name);

            listVehicles.Items.Clear();
            listVehicles.Update();
            listVehicles.Refresh();
            foreach (VehicleDTO vehicleDTO in zone.Vehicles)
            {
                ListViewItem vehicleItem = new ListViewItem(vehicleDTO.Vin);
                listVehicles.Items.Add(vehicleItem);
            }
        }
    }
}
