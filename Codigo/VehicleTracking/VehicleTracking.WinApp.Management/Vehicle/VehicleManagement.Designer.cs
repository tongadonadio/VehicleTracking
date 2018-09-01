namespace VehicleTracking.WinApp.Management
{
    partial class VehicleManagement
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstVehicles = new System.Windows.Forms.ListView();
            this.columnVin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.culmnBrand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnModel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnYear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addBtn = new System.Windows.Forms.Button();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 17);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(426, 47);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "Gestión de vehículos";
            // 
            // lstVehicles
            // 
            this.lstVehicles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnVin,
            this.culmnBrand,
            this.columnModel,
            this.columnYear,
            this.columnColor,
            this.columnType,
            this.columnStatus});
            this.lstVehicles.FullRowSelect = true;
            this.lstVehicles.Location = new System.Drawing.Point(20, 69);
            this.lstVehicles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstVehicles.Name = "lstVehicles";
            this.lstVehicles.Size = new System.Drawing.Size(812, 176);
            this.lstVehicles.TabIndex = 33;
            this.lstVehicles.UseCompatibleStateImageBehavior = false;
            this.lstVehicles.View = System.Windows.Forms.View.Details;
            // 
            // columnVin
            // 
            this.columnVin.Text = "Vin";
            this.columnVin.Width = 94;
            // 
            // culmnBrand
            // 
            this.culmnBrand.Text = "Marca";
            this.culmnBrand.Width = 112;
            // 
            // columnModel
            // 
            this.columnModel.Text = "Modelo";
            this.columnModel.Width = 112;
            // 
            // columnYear
            // 
            this.columnYear.Text = "Año";
            this.columnYear.Width = 62;
            // 
            // columnColor
            // 
            this.columnColor.Text = "Color";
            this.columnColor.Width = 65;
            // 
            // columnType
            // 
            this.columnType.Text = "Tipo";
            this.columnType.Width = 110;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Estado";
            this.columnStatus.Width = 227;
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.addBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(372, 274);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(128, 48);
            this.addBtn.TabIndex = 43;
            this.addBtn.Text = "Agregar";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // modifyBtn
            // 
            this.modifyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.modifyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyBtn.ForeColor = System.Drawing.Color.White;
            this.modifyBtn.Location = new System.Drawing.Point(20, 274);
            this.modifyBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(128, 48);
            this.modifyBtn.TabIndex = 44;
            this.modifyBtn.Text = "Modificar";
            this.modifyBtn.UseVisualStyleBackColor = false;
            this.modifyBtn.Click += new System.EventHandler(this.modifyBtn_Click);
            // 
            // delBtn
            // 
            this.delBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.delBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delBtn.ForeColor = System.Drawing.Color.White;
            this.delBtn.Location = new System.Drawing.Point(192, 274);
            this.delBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(128, 48);
            this.delBtn.TabIndex = 45;
            this.delBtn.Text = "Eliminar";
            this.delBtn.UseVisualStyleBackColor = false;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // VehicleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.modifyBtn);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstVehicles);
            this.Controls.Add(this.addBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VehicleManagement";
            this.Size = new System.Drawing.Size(1002, 508);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lstVehicles;
        private System.Windows.Forms.ColumnHeader columnVin;
        private System.Windows.Forms.ColumnHeader culmnBrand;
        private System.Windows.Forms.ColumnHeader columnModel;
        private System.Windows.Forms.ColumnHeader columnYear;
        private System.Windows.Forms.ColumnHeader columnColor;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button modifyBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.ColumnHeader columnStatus;
    }
}
