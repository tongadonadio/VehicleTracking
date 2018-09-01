using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Services;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Repository;
using VehicleTracking.Web.Models;
using VehicleTracking.WinApp.Management.Vehicle;
using VehicleTracking.WinApp.Management.Observer;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.ImporterOffline;
using VehicleTracking.ImporterOffline;
using VehicleTracking.Log.Events;
using VehicleTracking.Services.Log;
using Newtonsoft.Json;

namespace VehicleTracking.WinApp.Management
{
    public partial class VehicleManagement : UserControl, WindowsObserver
    {

        private VehicleService vehicleService;
        private VehicleTracking.Services.ImporterOffline.ImporterOffline importerService;
        private AddVehicles addVehiclesWindow;
        private ModifyVehicles modifyVehiclesWindow;
        private List<VehicleDTO> vehicles;
        private UserLoggedDTO userLogged;
        private Logs logs;

        public VehicleManagement(UserLoggedDTO user, Logs logs)
        {
            InitializeComponent();
            this.vehicleService = new VehicleServiceImpl(new VehicleDAOImpl(), new FlowDAOImp(), new ZoneDAOImp());
            this.userLogged = user;
            this.logs = logs;
            addVehiclesWindow = new AddVehicles(this, vehicleService);
            this.addBtn.FlatStyle = FlatStyle.Flat;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.delBtn.FlatStyle = FlatStyle.Flat;
            this.delBtn.FlatAppearance.BorderSize = 0;
            this.modifyBtn.FlatStyle = FlatStyle.Flat;
            this.modifyBtn.FlatAppearance.BorderSize = 0;
            importerService = new Services.ImporterOffline.ImporterOffline("Importers");
            LoadVehiclesImportsBtns();
            this.init();
        }

        private void init()
        {
            lstVehicles.Items.Clear();
            lstVehicles.Update();
            lstVehicles.Refresh();
            this.vehicles = this.vehicleService.GetAllVehicles();
            foreach(VehicleDTO vehicleDTO in vehicles)
            {
                ListViewItem vehicleItem = new ListViewItem(vehicleDTO.Vin);
                vehicleItem.SubItems.Add(vehicleDTO.Brand);
                vehicleItem.SubItems.Add(vehicleDTO.Model);
                vehicleItem.SubItems.Add(vehicleDTO.Year.ToString());
                vehicleItem.SubItems.Add(vehicleDTO.Color);
                vehicleItem.SubItems.Add(vehicleDTO.Type);
                vehicleItem.SubItems.Add(vehicleDTO.Status);

                lstVehicles.Items.Add(vehicleItem);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            this.addVehiclesWindow.ShowDialog();
        }

        public void UpdateForm()
        {
            this.init();
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (this.lstVehicles.SelectedItems.Count > 0)
            {
                String vin = this.lstVehicles.SelectedItems[0].Text;
                VehicleDTO selectedVehicle = this.vehicles.Find(v => v.Vin == vin);
                modifyVehiclesWindow = new ModifyVehicles(selectedVehicle, this, this.vehicleService);
                modifyVehiclesWindow.ShowDialog();
            } else
            {
                MessageBox.Show(
                    "Seleccione un vehículo de la lista.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (this.lstVehicles.SelectedItems.Count > 0)
            {
                String vin = this.lstVehicles.SelectedItems[0].Text;
                DialogResult dialogResult = MessageBox.Show(
                    "¿Está seguro que desea eliminar el vehiculo: "+vin+" ?",
                    "Cuidado",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dialogResult == DialogResult.Yes) { 
                    this.vehicleService.DeleteVehicle(vin);
                    this.init();
                }
            }
            else
            {
                MessageBox.Show(
                    "Seleccione un vehículo de la lista.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVehiclesImportsBtns()
        {
            int count = 0;
            foreach (var item in this.importerService.AllOfType<IVehicleImportOffline>())
            {
                Button button = CreateButton();
                button.Name = item.Name;
                button.Text = item.Name;
                button.Location = count == 0 ? new System.Drawing.Point(436, 178) : new System.Drawing.Point(436, 215);
                count++;

                button.Click += (sender, e) =>
                {
                    try
                    {
                        var vehicles = item.ImportVehicles();
                        var importedVehicles = 0;
                        foreach (var vehicle in vehicles)
                        {
                            vehicleService.AddVehicle(vehicle);
                            importedVehicles++;

                            UserDTO user = new UserDTO();
                            user.UserName = this.userLogged.FullName;
                            LogEvent log = new VehicleImportEvent(user, vehicle);
                            VehicleTrackingLog.GetInstance().WriteEvent(log);
                            this.logs.LoadLogs();
                        }
                        MessageBox.Show("Se han importado " + importedVehicles + " vehículos");
                        this.init();
                    }
                    catch(VehicleNullAttributesException)
                    {
                        MessageBox.Show(
                            "Los vehiculos que se intentan importar no tienen todos los datos",
                            "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } catch(VehicleVinDuplicatedException)
                    {
                        MessageBox.Show(
                            "Se encontraron vehículos ya registrados",
                            "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (JsonReaderException)
                    {
                        MessageBox.Show(
                              "El archivo no tiene un formato de json correcto",
                              "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch(InvalidOperationException)
                    {
                        MessageBox.Show(
                              "El archivo no tiene un formato de xml correcto",
                              "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException)
                    {
                        // No se hace nada, ya que se cierra el importador de archivos
                    }

                };
                this.Controls.Add(button);
            }
        }

        private Button CreateButton()
        {
            Button button = new Button();
            button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.ForeColor = System.Drawing.Color.White;
            button.Location = new System.Drawing.Point(436, 215);
            button.Size = new System.Drawing.Size(120, 31);
            button.TabIndex = 47;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
    }
}
