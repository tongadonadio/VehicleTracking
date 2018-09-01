namespace VehicleTracking.WinApp.Management.Flow
{
    partial class NewFlow
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
            this.addType = new System.Windows.Forms.Button();
            this.lblSubzoneTypes = new System.Windows.Forms.Label();
            this.listFlow = new System.Windows.Forms.ListView();
            this.columnOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLbl = new System.Windows.Forms.Label();
            this.listTypes = new System.Windows.Forms.ListView();
            this.columnSubZoneType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTitle = new System.Windows.Forms.Label();
            this.removeType = new System.Windows.Forms.Button();
            this.newFlowBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1487, 100);
            this.panel1.TabIndex = 5;
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
            // addType
            // 
            this.addType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.addType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addType.ForeColor = System.Drawing.Color.White;
            this.addType.Location = new System.Drawing.Point(545, 321);
            this.addType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addType.Name = "addType";
            this.addType.Size = new System.Drawing.Size(122, 48);
            this.addType.TabIndex = 53;
            this.addType.Text = "<";
            this.addType.UseVisualStyleBackColor = false;
            this.addType.Click += new System.EventHandler(this.addType_Click);
            // 
            // lblSubzoneTypes
            // 
            this.lblSubzoneTypes.AutoSize = true;
            this.lblSubzoneTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubzoneTypes.Location = new System.Drawing.Point(780, 196);
            this.lblSubzoneTypes.Name = "lblSubzoneTypes";
            this.lblSubzoneTypes.Size = new System.Drawing.Size(234, 29);
            this.lblSubzoneTypes.TabIndex = 52;
            this.lblSubzoneTypes.Text = "Tipos de subzonas";
            // 
            // listFlow
            // 
            this.listFlow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnOrder,
            this.columnType});
            this.listFlow.FullRowSelect = true;
            this.listFlow.Location = new System.Drawing.Point(150, 243);
            this.listFlow.Name = "listFlow";
            this.listFlow.Size = new System.Drawing.Size(278, 320);
            this.listFlow.TabIndex = 51;
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
            // flowLbl
            // 
            this.flowLbl.AutoSize = true;
            this.flowLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLbl.Location = new System.Drawing.Point(145, 196);
            this.flowLbl.Name = "flowLbl";
            this.flowLbl.Size = new System.Drawing.Size(72, 29);
            this.flowLbl.TabIndex = 50;
            this.flowLbl.Text = "Flujo";
            // 
            // listTypes
            // 
            this.listTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSubZoneType});
            this.listTypes.FullRowSelect = true;
            this.listTypes.Location = new System.Drawing.Point(785, 243);
            this.listTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listTypes.Name = "listTypes";
            this.listTypes.Size = new System.Drawing.Size(284, 320);
            this.listTypes.TabIndex = 49;
            this.listTypes.UseCompatibleStateImageBehavior = false;
            this.listTypes.View = System.Windows.Forms.View.Details;
            // 
            // columnSubZoneType
            // 
            this.columnSubZoneType.Text = "Tipos";
            this.columnSubZoneType.Width = 220;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 127);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(239, 47);
            this.lblTitle.TabIndex = 48;
            this.lblTitle.Text = "Nuevo flujo";
            // 
            // removeType
            // 
            this.removeType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.removeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeType.ForeColor = System.Drawing.Color.White;
            this.removeType.Location = new System.Drawing.Point(545, 465);
            this.removeType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeType.Name = "removeType";
            this.removeType.Size = new System.Drawing.Size(122, 48);
            this.removeType.TabIndex = 54;
            this.removeType.Text = ">";
            this.removeType.UseVisualStyleBackColor = false;
            this.removeType.Click += new System.EventHandler(this.removeType_Click);
            // 
            // newFlowBtn
            // 
            this.newFlowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.newFlowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newFlowBtn.ForeColor = System.Drawing.Color.White;
            this.newFlowBtn.Location = new System.Drawing.Point(532, 604);
            this.newFlowBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.newFlowBtn.Name = "newFlowBtn";
            this.newFlowBtn.Size = new System.Drawing.Size(146, 48);
            this.newFlowBtn.TabIndex = 55;
            this.newFlowBtn.Text = "Crear flujo";
            this.newFlowBtn.UseVisualStyleBackColor = false;
            this.newFlowBtn.Click += new System.EventHandler(this.newFlowBtn_Click);
            // 
            // NewFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1478, 1028);
            this.Controls.Add(this.newFlowBtn);
            this.Controls.Add(this.removeType);
            this.Controls.Add(this.addType);
            this.Controls.Add(this.lblSubzoneTypes);
            this.Controls.Add(this.listFlow);
            this.Controls.Add(this.flowLbl);
            this.Controls.Add(this.listTypes);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Name = "NewFlow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewFlow";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button addType;
        private System.Windows.Forms.Label lblSubzoneTypes;
        private System.Windows.Forms.ListView listFlow;
        private System.Windows.Forms.ColumnHeader columnOrder;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.Label flowLbl;
        private System.Windows.Forms.ListView listTypes;
        private System.Windows.Forms.ColumnHeader columnSubZoneType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button removeType;
        private System.Windows.Forms.Button newFlowBtn;
    }
}