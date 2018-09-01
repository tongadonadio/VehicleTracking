namespace VehicleTracking.WinApp.Management.Zone
{
    partial class AddZone
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
            this.numericCapacity = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.typeLbl = new System.Windows.Forms.Label();
            this.isSubZoneLbl = new System.Windows.Forms.Label();
            this.capacityLbl = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.isSubZoneChk = new System.Windows.Forms.CheckBox();
            this.comboTypes = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1479, 100);
            this.panel1.TabIndex = 8;
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
            // numericCapacity
            // 
            this.numericCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCapacity.Location = new System.Drawing.Point(348, 360);
            this.numericCapacity.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericCapacity.Name = "numericCapacity";
            this.numericCapacity.Size = new System.Drawing.Size(577, 35);
            this.numericCapacity.TabIndex = 70;
            this.numericCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(347, 210);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(576, 35);
            this.txtName.TabIndex = 69;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(342, 178);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(117, 29);
            this.nameLbl.TabIndex = 68;
            this.nameLbl.Text = "* Nombre";
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.ForeColor = System.Drawing.Color.White;
            this.cancelBtn.Location = new System.Drawing.Point(658, 557);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(178, 75);
            this.cancelBtn.TabIndex = 67;
            this.cancelBtn.Text = "Cancelar";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLbl.Location = new System.Drawing.Point(351, 402);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(85, 29);
            this.typeLbl.TabIndex = 65;
            this.typeLbl.Text = "* Tipo:";
            // 
            // isSubZoneLbl
            // 
            this.isSubZoneLbl.AutoSize = true;
            this.isSubZoneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isSubZoneLbl.Location = new System.Drawing.Point(342, 253);
            this.isSubZoneLbl.Name = "isSubZoneLbl";
            this.isSubZoneLbl.Size = new System.Drawing.Size(129, 29);
            this.isSubZoneLbl.TabIndex = 62;
            this.isSubZoneLbl.Text = "* Subzona:";
            // 
            // capacityLbl
            // 
            this.capacityLbl.AutoSize = true;
            this.capacityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capacityLbl.Location = new System.Drawing.Point(351, 328);
            this.capacityLbl.Name = "capacityLbl";
            this.capacityLbl.Size = new System.Drawing.Size(151, 29);
            this.capacityLbl.TabIndex = 64;
            this.capacityLbl.Text = "* Capacidad:";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.addBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(402, 557);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(178, 75);
            this.addBtn.TabIndex = 60;
            this.addBtn.Text = "Agregar";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // isSubZoneChk
            // 
            this.isSubZoneChk.AutoSize = true;
            this.isSubZoneChk.Location = new System.Drawing.Point(356, 291);
            this.isSubZoneChk.Name = "isSubZoneChk";
            this.isSubZoneChk.Size = new System.Drawing.Size(137, 24);
            this.isSubZoneChk.TabIndex = 71;
            this.isSubZoneChk.Text = "¿Es subzona?";
            this.isSubZoneChk.UseVisualStyleBackColor = true;
            this.isSubZoneChk.CheckedChanged += new System.EventHandler(this.isSubZoneChk_CheckedChanged);
            // 
            // comboTypes
            // 
            this.comboTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypes.FormattingEnabled = true;
            this.comboTypes.Location = new System.Drawing.Point(351, 445);
            this.comboTypes.Name = "comboTypes";
            this.comboTypes.Size = new System.Drawing.Size(574, 37);
            this.comboTypes.TabIndex = 72;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(13, 112);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(235, 47);
            this.lblTitle.TabIndex = 73;
            this.lblTitle.Text = "Crear zona";
            // 
            // AddZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1478, 1028);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.comboTypes);
            this.Controls.Add(this.isSubZoneChk);
            this.Controls.Add(this.numericCapacity);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.isSubZoneLbl);
            this.Controls.Add(this.capacityLbl);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.panel1);
            this.Name = "AddZone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddZone";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.NumericUpDown numericCapacity;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.Label isSubZoneLbl;
        private System.Windows.Forms.Label capacityLbl;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.CheckBox isSubZoneChk;
        private System.Windows.Forms.ComboBox comboTypes;
        private System.Windows.Forms.Label lblTitle;
    }
}