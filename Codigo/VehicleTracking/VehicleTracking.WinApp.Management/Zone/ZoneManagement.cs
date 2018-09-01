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
using VehicleTracking.WinApp.Management.Zone;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.Services.Exceptions;

namespace VehicleTracking.WinApp.Management
{
    public partial class ZoneManagement : UserControl, WindowsObserver
    {

        private ZoneService zoneService;
        private FlowService flowService;
        private List<ZoneDTO> zones;
        private List<ZoneDTO> subZones;

        public ZoneManagement()
        {
            ZoneDAO zoneDao = new ZoneDAOImp();
            VehicleDAO vehicleDao = new VehicleDAOImpl();
            FlowDAO flowDao = new FlowDAOImp();

            this.zoneService = new ZoneServiceImp(zoneDao, flowDao, vehicleDao);
            this.flowService = new FlowServiceImp(flowDao);
            InitializeComponent();
            this.addBtn.FlatStyle = FlatStyle.Flat;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.modifyBtn.FlatStyle = FlatStyle.Flat;
            this.modifyBtn.FlatAppearance.BorderSize = 0;
            this.delBtn.FlatStyle = FlatStyle.Flat;
            this.delBtn.FlatAppearance.BorderSize = 0;
            this.detailsBtn.FlatStyle = FlatStyle.Flat;
            this.detailsBtn.FlatAppearance.BorderSize = 0;
            this.assignBtn.FlatStyle = FlatStyle.Flat;
            this.assignBtn.FlatAppearance.BorderSize = 0;
            this.init();
        }

        private void init()
        {
            this.zones = new List<ZoneDTO>();
            this.subZones = new List<ZoneDTO>();

            List<ZoneDTO> obtainedZones = this.zoneService.GetAllZones();
            this.zones = obtainedZones.FindAll(z => !z.IsSubZone);
            this.subZones = obtainedZones.FindAll(sz => sz.IsSubZone);
            
            this.loadSubZones();
            this.loadZones();
        }

        private void loadZones()
        {
            this.treeZones.Nodes.Clear();
            this.treeZones.Update();
            this.treeZones.Refresh();

            List<TreeNode> roots = new List<TreeNode>();
            roots.Add(this.treeZones.Nodes.Add("Zonas"));
            foreach (ZoneDTO zone in this.zones)
            {
                TreeNode zoneNode = roots[0].Nodes.Add(zone.Name);
                zoneNode.Tag = zone;
                foreach (ZoneDTO subZone in zone.SubZones)
                {
                    TreeNode subZoneNode = zoneNode.Nodes.Add(subZone.Name);
                    subZoneNode.Tag = subZone;
                }
                roots.Add(zoneNode);
            }
            this.treeZones.Nodes[0].ExpandAll();
        }

        private void loadSubZones()
        {
            listSubZones.Items.Clear();
            listSubZones.Update();
            listSubZones.Refresh();
            foreach (ZoneDTO subZoneDTO in this.subZones)
            {
                ListViewItem subZoneItem = new ListViewItem(subZoneDTO.Name);
                subZoneItem.Tag = subZoneDTO;

                listSubZones.Items.Add(subZoneItem);
            }
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (this.treeZones.SelectedNode != null && ((ZoneDTO)this.treeZones.SelectedNode.Tag) != null)
            {
                ZoneDTO zoneToModify = ((ZoneDTO)this.treeZones.SelectedNode.Tag);
                ModifyZone modifyZoneWindows = new ModifyZone(zoneToModify, this, this.zoneService);
                modifyZoneWindows.ShowDialog();
            }
            else
            {
                if (this.listSubZones.SelectedItems.Count > 0)
                {
                    ZoneDTO zoneToModify = ((ZoneDTO)this.listSubZones.SelectedItems[0].Tag);
                    ModifyZone modifyZoneWindows = new ModifyZone(zoneToModify, this, this.zoneService);
                    modifyZoneWindows.ShowDialog();
                }
                else
                {
                    MessageBox.Show(
                    "Seleccione una zona o subzona.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void detailsBtn_Click(object sender, EventArgs e)
        {
            if (this.treeZones.SelectedNode != null && ((ZoneDTO)this.treeZones.SelectedNode.Tag) != null)
            {
                ZoneDetails zoneDetailsWindows = new ZoneDetails((ZoneDTO)this.treeZones.SelectedNode.Tag);
                zoneDetailsWindows.ShowDialog();
            }
            else
            {
                if (this.listSubZones.SelectedItems.Count > 0)
                {
                    ZoneDetails zoneDetailsWindows = new ZoneDetails((ZoneDTO)this.listSubZones.SelectedItems[0].Tag);
                    zoneDetailsWindows.ShowDialog();
                }
                else
                {
                    MessageBox.Show(
                    "Seleccione una zona o subzona.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void UpdateForm()
        {
            this.init();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            AddZone addZoneWindows = new AddZone(this.zoneService, this.flowService, this);
            addZoneWindows.ShowDialog();
        }

        private void assignBtn_Click(object sender, EventArgs e)
        {
            if (this.listSubZones.SelectedItems.Count > 0)
            {
                AssignSubZone assignSubZoneWindows = new AssignSubZone((ZoneDTO)this.listSubZones.SelectedItems[0].Tag, this.zones, this.zoneService, this);
                assignSubZoneWindows.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                "Seleccione una subzona.",
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listSubZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeZones.SelectedNode = null;
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (this.treeZones.SelectedNode != null && ((ZoneDTO)this.treeZones.SelectedNode.Tag) != null)
            {
                try
                {
                    ZoneDTO zoneToDelete = ((ZoneDTO)this.treeZones.SelectedNode.Tag);
                    DialogResult dialogResult = MessageBox.Show(
                    "¿Está seguro que desea eliminar la zona: " + zoneToDelete.Id + " ?",
                    "Cuidado",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.zoneService.Delete(zoneToDelete.Id);
                        this.init();
                    }
                }
                catch (ZoneCannotBeDeletedException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (this.listSubZones.SelectedItems.Count > 0)
                {
                    try
                    {
                        ZoneDTO zoneToDelete = ((ZoneDTO)this.listSubZones.SelectedItems[0].Tag);
                        DialogResult dialogResult = MessageBox.Show(
                        "¿Está seguro que desea eliminar la zona: " + zoneToDelete.Id + " ?",
                        "Cuidado",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            this.zoneService.Delete(zoneToDelete.Id);
                            this.init();
                        }
                    }
                    catch (ZoneCannotBeDeletedException ex)
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
                    "Seleccione una zona o subzona.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
