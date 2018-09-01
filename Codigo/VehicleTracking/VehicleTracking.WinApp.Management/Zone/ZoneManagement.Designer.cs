namespace VehicleTracking.WinApp.Management
{
    partial class ZoneManagement
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeZones = new System.Windows.Forms.TreeView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.listSubZones = new System.Windows.Forms.ListView();
            this.zonesLbl = new System.Windows.Forms.Label();
            this.subZonesLbl = new System.Windows.Forms.Label();
            this.delBtn = new System.Windows.Forms.Button();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.detailsBtn = new System.Windows.Forms.Button();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assignBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeZones
            // 
            this.treeZones.Location = new System.Drawing.Point(30, 84);
            this.treeZones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeZones.Name = "treeZones";
            this.treeZones.Size = new System.Drawing.Size(302, 163);
            this.treeZones.TabIndex = 23;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 13);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(358, 47);
            this.lblTitle.TabIndex = 22;
            this.lblTitle.Text = "Gestión de zonas";
            // 
            // listSubZones
            // 
            this.listSubZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName});
            this.listSubZones.Location = new System.Drawing.Point(398, 84);
            this.listSubZones.Name = "listSubZones";
            this.listSubZones.Size = new System.Drawing.Size(316, 163);
            this.listSubZones.TabIndex = 24;
            this.listSubZones.UseCompatibleStateImageBehavior = false;
            this.listSubZones.View = System.Windows.Forms.View.Details;
            this.listSubZones.SelectedIndexChanged += new System.EventHandler(this.listSubZones_SelectedIndexChanged);
            // 
            // zonesLbl
            // 
            this.zonesLbl.AutoSize = true;
            this.zonesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zonesLbl.Location = new System.Drawing.Point(25, 50);
            this.zonesLbl.Name = "zonesLbl";
            this.zonesLbl.Size = new System.Drawing.Size(79, 29);
            this.zonesLbl.TabIndex = 25;
            this.zonesLbl.Text = "Zonas";
            // 
            // subZonesLbl
            // 
            this.subZonesLbl.AutoSize = true;
            this.subZonesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subZonesLbl.Location = new System.Drawing.Point(393, 50);
            this.subZonesLbl.Name = "subZonesLbl";
            this.subZonesLbl.Size = new System.Drawing.Size(119, 29);
            this.subZonesLbl.TabIndex = 26;
            this.subZonesLbl.Text = "Subzonas";
            // 
            // delBtn
            // 
            this.delBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.delBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delBtn.ForeColor = System.Drawing.Color.White;
            this.delBtn.Location = new System.Drawing.Point(184, 278);
            this.delBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(128, 48);
            this.delBtn.TabIndex = 48;
            this.delBtn.Text = "Eliminar";
            this.delBtn.UseVisualStyleBackColor = false;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // modifyBtn
            // 
            this.modifyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.modifyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyBtn.ForeColor = System.Drawing.Color.White;
            this.modifyBtn.Location = new System.Drawing.Point(40, 278);
            this.modifyBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(128, 48);
            this.modifyBtn.TabIndex = 47;
            this.modifyBtn.Text = "Modificar";
            this.modifyBtn.UseVisualStyleBackColor = false;
            this.modifyBtn.Click += new System.EventHandler(this.modifyBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.addBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(476, 278);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(128, 48);
            this.addBtn.TabIndex = 46;
            this.addBtn.Text = "Agregar";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // detailsBtn
            // 
            this.detailsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.detailsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsBtn.ForeColor = System.Drawing.Color.White;
            this.detailsBtn.Location = new System.Drawing.Point(328, 278);
            this.detailsBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new System.Drawing.Size(128, 48);
            this.detailsBtn.TabIndex = 49;
            this.detailsBtn.Text = "Detalles";
            this.detailsBtn.UseVisualStyleBackColor = false;
            this.detailsBtn.Click += new System.EventHandler(this.detailsBtn_Click);
            // 
            // columnName
            // 
            this.columnName.Text = "Nombre";
            this.columnName.Width = 305;
            // 
            // assignBtn
            // 
            this.assignBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.assignBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignBtn.ForeColor = System.Drawing.Color.White;
            this.assignBtn.Location = new System.Drawing.Point(618, 278);
            this.assignBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.assignBtn.Name = "assignBtn";
            this.assignBtn.Size = new System.Drawing.Size(183, 48);
            this.assignBtn.TabIndex = 50;
            this.assignBtn.Text = "Asignar subzona";
            this.assignBtn.UseVisualStyleBackColor = false;
            this.assignBtn.Click += new System.EventHandler(this.assignBtn_Click);
            // 
            // ZoneManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.assignBtn);
            this.Controls.Add(this.detailsBtn);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.modifyBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.subZonesLbl);
            this.Controls.Add(this.zonesLbl);
            this.Controls.Add(this.listSubZones);
            this.Controls.Add(this.treeZones);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ZoneManagement";
            this.Size = new System.Drawing.Size(884, 477);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeZones;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView listSubZones;
        private System.Windows.Forms.Label zonesLbl;
        private System.Windows.Forms.Label subZonesLbl;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button modifyBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button detailsBtn;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button assignBtn;
    }
}
