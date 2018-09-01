using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Log;
using VehicleTracking.Services.Log;
using VehicleTracking.Log.Events;

namespace VehicleTracking.WinApp.Management
{
    public partial class Logs : UserControl
    {
        ILog log;

        public Logs()
        {
            InitializeComponent();
            LoadLogs();
        }

        public void LoadLogs()
        {
            this.log = VehicleTrackingLog.GetInstance();
            List<LogEvent> logs = this.log.FindEvents(le => true);
            foreach (var actualLog in logs)
            {
                ListViewItem lstViewItem = new ListViewItem(
                    new[] { actualLog.Type,
                        actualLog.Date.ToString(),
                        actualLog.User.UserName,
                        actualLog.Content});
                lstViewLogs.Items.Add(lstViewItem);
            }
        }

        private void dtPickerFrom_ValueChanged(object sender, EventArgs e)
        {
            filteredLogs();
        }

        private void dtPickerTo_ValueChanged(object sender, EventArgs e)
        {
            filteredLogs();
        }

        private void filteredLogs()
        {
            DateTime? dateFrom = dtPickerFrom.Value;
            DateTime? dateTo = dtPickerTo.Value;

            var logs = this.log.FindEvents(le => (!dateFrom.HasValue || dateFrom <= le.Date) && (!dateTo.HasValue || dateTo >= le.Date));
            lstViewLogs.Items.Clear();
            foreach (var actualLog in logs)
            {
                ListViewItem lstViewItem = new ListViewItem(
                    new[] { actualLog.Type,
                        actualLog.Date.ToString(),
                        actualLog.User.UserName,
                        actualLog.Content});
                lstViewLogs.Items.Add(lstViewItem);
            }
        }
    }
}
