namespace VehicleTracking.WinApp.Management.Zone
{
    partial class ZoneDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.idLbl = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.isSubZoneLbl = new System.Windows.Forms.Label();
            this.capacityLbl = new System.Windows.Forms.Label();
            this.typeLbl = new System.Windows.Forms.Label();
            this.vehiclesLbl = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.listVehicles = new System.Windows.Forms.ListView();
            this.columnVin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1479, 100);
            this.panel1.TabIndex = 7;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(54, 23);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(362, 52);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Vehicle Tracking";
            // 
            // idLbl
            // 
            this.idLbl.AutoSize = true;
            this.idLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLbl.Location = new System.Drawing.Point(360, 185);
            this.idLbl.Name = "idLbl";
            this.idLbl.Size = new System.Drawing.Size(46, 32);
            this.idLbl.TabIndex = 8;
            this.idLbl.Text = "Id:";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(360, 246);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(123, 32);
            this.nameLbl.TabIndex = 9;
            this.nameLbl.Text = "Nombre:";
            // 
            // isSubZoneLbl
            // 
            this.isSubZoneLbl.AutoSize = true;
            this.isSubZoneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isSubZoneLbl.Location = new System.Drawing.Point(360, 306);
            this.isSubZoneLbl.Name = "isSubZoneLbl";
            this.isSubZoneLbl.Size = new System.Drawing.Size(171, 32);
            this.isSubZoneLbl.TabIndex = 10;
            this.isSubZoneLbl.Text = "Es subzona:";
            // 
            // capacityLbl
            // 
            this.capacityLbl.AutoSize = true;
            this.capacityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capacityLbl.Location = new System.Drawing.Point(360, 361);
            this.capacityLbl.Name = "capacityLbl";
            this.capacityLbl.Size = new System.Drawing.Size(160, 32);
            this.capacityLbl.TabIndex = 11;
            this.capacityLbl.Text = "Capacidad:";
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLbl.Location = new System.Drawing.Point(360, 418);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(79, 32);
            this.typeLbl.TabIndex = 12;
            this.typeLbl.Text = "Tipo:";
            // 
            // vehiclesLbl
            // 
            this.vehiclesLbl.AutoSize = true;
            this.vehiclesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vehiclesLbl.Location = new System.Drawing.Point(360, 473);
            this.vehiclesLbl.Name = "vehiclesLbl";
            this.vehiclesLbl.Size = new System.Drawing.Size(148, 32);
            this.vehiclesLbl.TabIndex = 13;
            this.vehiclesLbl.Text = "Vehículos:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(13, 107);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(323, 47);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "Detalle de zona";
            // 
            // listVehicles
            // 
            this.listVehicles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnVin});
            this.listVehicles.Location = new System.Drawing.Point(366, 508);
            this.listVehicles.Name = "listVehicles";
            this.listVehicles.Size = new System.Drawing.Size(498, 117);
            this.listVehicles.TabIndex = 24;
            this.listVehicles.UseCompatibleStateImageBehavior = false;
            this.listVehicles.View = System.Windows.Forms.View.Details;
            // 
            // columnVin
            // 
            this.columnVin.Text = "Vin";
            this.columnVin.Width = 493;
            // 
            // ZoneDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1478, 1028);
            this.Controls.Add(this.listVehicles);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.vehiclesLbl);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.capacityLbl);
            this.Controls.Add(this.isSubZoneLbl);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.idLbl);
            this.Controls.Add(this.panel1);
            this.Name = "ZoneDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZoneDetails";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label idLbl;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label isSubZoneLbl;
        private System.Windows.Forms.Label capacityLbl;
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.Label vehiclesLbl;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView listVehicles;
        private System.Windows.Forms.ColumnHeader columnVin;
    }
}