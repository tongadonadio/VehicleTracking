using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.WinApp.Management.Subject;

namespace VehicleTracking.WinApp.Management.Zone
{

    public partial class AssignSubZone : Form
    {

        private ZoneDTO subZoneToAssign;
        private WindowsSubject subject;
        private ZoneService zoneService;
        private List<ZoneDTO> zones;

        public AssignSubZone(ZoneDTO subZone, List<ZoneDTO> zones, ZoneService zoneService, WindowsObserver observer)
        {
            this.subject = new WindowsSubject();
            this.subject.Attach(observer);
            this.subZoneToAssign = subZone;
            this.zoneService = zoneService;
            this.zones = zones;
            InitializeComponent();
            this.assignBtn.FlatStyle = FlatStyle.Flat;
            this.assignBtn.FlatAppearance.BorderSize = 0;
            this.loadZones();
        }

        private void loadZones()
        {
            listZones.Items.Clear();
            listZones.Update();
            listZones.Refresh();
            foreach (ZoneDTO zoneDTO in this.zones)
            {
                ListViewItem zoneItem = new ListViewItem(zoneDTO.Id.ToString());
                zoneItem.Tag = zoneDTO;
                zoneItem.SubItems.Add(zoneDTO.Name);

                this.listZones.Items.Add(zoneItem);
            }
        }

        private void assignBtn_Click(object sender, EventArgs e)
        {
            if (this.listZones.SelectedItems.Count > 0)
            {
                try
                {
                    this.zoneService.AssignZone(this.subZoneToAssign.Id, ((ZoneDTO)this.listZones.SelectedItems[0].Tag).Id);
                    this.subject.Notify();
                    this.Close();
                }
                catch (AssignVehicleToMainZoneException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (AssignVehicleToZoneWithoutCapacityException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ZoneCannotBeSubzoneOfAnotherSubzoneException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ZoneOutOfCapacityException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Seleccione una zona.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
