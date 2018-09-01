namespace VehicleTracking.WinApp.Management.Zone
{
    partial class AssignSubZone
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
            this.zonesLbl = new System.Windows.Forms.Label();
            this.listZones = new System.Windows.Forms.ListView();
            this.columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assignBtn = new System.Windows.Forms.Button();
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
            this.panel1.TabIndex = 9;
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
            // zonesLbl
            // 
            this.zonesLbl.AutoSize = true;
            this.zonesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zonesLbl.Location = new System.Drawing.Point(261, 169);
            this.zonesLbl.Name = "zonesLbl";
            this.zonesLbl.Size = new System.Drawing.Size(79, 29);
            this.zonesLbl.TabIndex = 28;
            this.zonesLbl.Text = "Zonas";
            // 
            // listZones
            // 
            this.listZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnId,
            this.columnName});
            this.listZones.FullRowSelect = true;
            this.listZones.Location = new System.Drawing.Point(266, 201);
            this.listZones.Name = "listZones";
            this.listZones.Size = new System.Drawing.Size(692, 293);
            this.listZones.TabIndex = 29;
            this.listZones.UseCompatibleStateImageBehavior = false;
            this.listZones.View = System.Windows.Forms.View.Details;
            // 
            // columnId
            // 
            this.columnId.Text = "Id";
            this.columnId.Width = 227;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 140;
            // 
            // assignBtn
            // 
            this.assignBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.assignBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignBtn.ForeColor = System.Drawing.Color.White;
            this.assignBtn.Location = new System.Drawing.Point(510, 531);
            this.assignBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.assignBtn.Name = "assignBtn";
            this.assignBtn.Size = new System.Drawing.Size(178, 75);
            this.assignBtn.TabIndex = 61;
            this.assignBtn.Text = "Asignar";
            this.assignBtn.UseVisualStyleBackColor = false;
            this.assignBtn.Click += new System.EventHandler(this.assignBtn_Click);
            // 
            // AssignSubZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1478, 1028);
            this.Controls.Add(this.assignBtn);
            this.Controls.Add(this.listZones);
            this.Controls.Add(this.zonesLbl);
            this.Controls.Add(this.panel1);
            this.Name = "AssignSubZone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssignSubZone";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label zonesLbl;
        private System.Windows.Forms.ListView listZones;
        private System.Windows.Forms.ColumnHeader columnId;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button assignBtn;
    }
}