namespace VehicleTracking.WinApp.Management
{
    partial class FlowManagement
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
            this.listTypes = new System.Windows.Forms.ListView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowLbl = new System.Windows.Forms.Label();
            this.listFlow = new System.Windows.Forms.ListView();
            this.columnOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblSubzoneTypes = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.newFlow = new System.Windows.Forms.Button();
            this.columnSubZoneType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblNewType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listTypes
            // 
            this.listTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSubZoneType});
            this.listTypes.Location = new System.Drawing.Point(333, 103);
            this.listTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listTypes.Name = "listTypes";
            this.listTypes.Size = new System.Drawing.Size(229, 174);
            this.listTypes.TabIndex = 29;
            this.listTypes.UseCompatibleStateImageBehavior = false;
            this.listTypes.View = System.Windows.Forms.View.Details;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 47);
            this.lblTitle.TabIndex = 27;
            this.lblTitle.Text = "Gestión de flujos";
            // 
            // flowLbl
            // 
            this.flowLbl.AutoSize = true;
            this.flowLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLbl.Location = new System.Drawing.Point(25, 71);
            this.flowLbl.Name = "flowLbl";
            this.flowLbl.Size = new System.Drawing.Size(148, 29);
            this.flowLbl.TabIndex = 32;
            this.flowLbl.Text = "Flujo actual";
            // 
            // listFlow
            // 
            this.listFlow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnOrder,
            this.columnType});
            this.listFlow.Location = new System.Drawing.Point(30, 103);
            this.listFlow.Name = "listFlow";
            this.listFlow.Size = new System.Drawing.Size(236, 174);
            this.listFlow.TabIndex = 33;
            this.listFlow.UseCompatibleStateImageBehavior = false;
            this.listFlow.View = System.Windows.Forms.View.Details;
            // 
            // columnOrder
            // 
            this.columnOrder.Text = "Orden";
            // 
            // columnType
            // 
            this.columnType.Text = "Tipo de subzona";
            this.columnType.Width = 165;
            // 
            // lblSubzoneTypes
            // 
            this.lblSubzoneTypes.AutoSize = true;
            this.lblSubzoneTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubzoneTypes.Location = new System.Drawing.Point(328, 69);
            this.lblSubzoneTypes.Name = "lblSubzoneTypes";
            this.lblSubzoneTypes.Size = new System.Drawing.Size(234, 29);
            this.lblSubzoneTypes.TabIndex = 34;
            this.lblSubzoneTypes.Text = "Tipos de subzonas";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.addBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(592, 189);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(167, 48);
            this.addBtn.TabIndex = 44;
            this.addBtn.Text = "Agregar Tipo";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // newFlow
            // 
            this.newFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.newFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newFlow.ForeColor = System.Drawing.Color.White;
            this.newFlow.Location = new System.Drawing.Point(62, 297);
            this.newFlow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.newFlow.Name = "newFlow";
            this.newFlow.Size = new System.Drawing.Size(174, 48);
            this.newFlow.TabIndex = 47;
            this.newFlow.Text = "Nuevo flujo";
            this.newFlow.UseVisualStyleBackColor = false;
            this.newFlow.Click += new System.EventHandler(this.newFlow_Click);
            // 
            // columnSubZoneType
            // 
            this.columnSubZoneType.Text = "Tipos";
            this.columnSubZoneType.Width = 220;
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(592, 131);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(165, 35);
            this.txtType.TabIndex = 48;
            // 
            // lblNewType
            // 
            this.lblNewType.AutoSize = true;
            this.lblNewType.Location = new System.Drawing.Point(588, 108);
            this.lblNewType.Name = "lblNewType";
            this.lblNewType.Size = new System.Drawing.Size(130, 20);
            this.lblNewType.TabIndex = 49;
            this.lblNewType.Text = "Tipo de subzona:";
            // 
            // FlowManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNewType);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.newFlow);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.lblSubzoneTypes);
            this.Controls.Add(this.listFlow);
            this.Controls.Add(this.flowLbl);
            this.Controls.Add(this.listTypes);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FlowManagement";
            this.Size = new System.Drawing.Size(966, 477);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listTypes;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label flowLbl;
        private System.Windows.Forms.ListView listFlow;
        private System.Windows.Forms.ColumnHeader columnOrder;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.Label lblSubzoneTypes;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button newFlow;
        private System.Windows.Forms.ColumnHeader columnSubZoneType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblNewType;
    }
}
