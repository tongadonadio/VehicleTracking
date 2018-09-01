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

namespace VehicleTracking.WinApp.Management
{
    public partial class Main : Form
    {
        UserLoggedDTO userLogged;

        public Main(UserLoggedDTO user)
        {
            InitializeComponent();
            this.userLogged = user;
            this.init(); 
        }

        private void init()
        {
            Logs logs = new Logs();
            panelLog.Controls.Clear();
            panelLog.Controls.Add(logs);

            VehicleManagement vehicleManagement = new VehicleManagement(this.userLogged, logs);
            panelVehicle.Controls.Clear();
            panelVehicle.Controls.Add(vehicleManagement);

            ZoneManagement zoneManagement = new ZoneManagement();
            panelZones.Controls.Clear();
            panelZones.Controls.Add(zoneManagement);

            FlowManagement flowManagement = new FlowManagement();
            panelFlow.Controls.Clear();
            panelFlow.Controls.Add(flowManagement);
        }

        private void menuLog_Click(object sender, EventArgs e)
        {
            Logs logs = new Logs();
            panelVehicle.Controls.Clear();
            panelVehicle.Controls.Add(logs);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelVehicle.Controls.Clear();
            this.Close();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
