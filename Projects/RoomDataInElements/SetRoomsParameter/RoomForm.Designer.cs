namespace SetRoomsParameter
{
    partial class RoomForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btn_AddNewParameter = new System.Windows.Forms.Button();
            this.ParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterStorageTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSharedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 332);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters to copy data";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 311);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterNameColumn,
            this.ParameterStorageTypeColumn,
            this.ParameterIDColumn,
            this.IsSharedColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(769, 311);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(796, 83);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 37);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btn_AddNewParameter
            // 
            this.btn_AddNewParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddNewParameter.Location = new System.Drawing.Point(796, 30);
            this.btn_AddNewParameter.Name = "btn_AddNewParameter";
            this.btn_AddNewParameter.Size = new System.Drawing.Size(108, 37);
            this.btn_AddNewParameter.TabIndex = 1;
            this.btn_AddNewParameter.Text = "A&dd";
            this.btn_AddNewParameter.UseVisualStyleBackColor = true;
            this.btn_AddNewParameter.Click += new System.EventHandler(this.btn_AddNewParameter_Click);
            // 
            // ParameterNameColumn
            // 
            this.ParameterNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterNameColumn.DataPropertyName = "Name";
            this.ParameterNameColumn.HeaderText = "Parameter";
            this.ParameterNameColumn.Name = "ParameterNameColumn";
            this.ParameterNameColumn.ReadOnly = true;
            // 
            // ParameterStorageTypeColumn
            // 
            this.ParameterStorageTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterStorageTypeColumn.DataPropertyName = "Type";
            this.ParameterStorageTypeColumn.HeaderText = "Storage Type";
            this.ParameterStorageTypeColumn.Name = "ParameterStorageTypeColumn";
            this.ParameterStorageTypeColumn.ReadOnly = true;
            // 
            // ParameterIDColumn
            // 
            this.ParameterIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterIDColumn.DataPropertyName = "ID";
            this.ParameterIDColumn.HeaderText = "Parameter ID";
            this.ParameterIDColumn.Name = "ParameterIDColumn";
            this.ParameterIDColumn.ReadOnly = true;
            this.ParameterIDColumn.Visible = false;
            // 
            // IsSharedColumn
            // 
            this.IsSharedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsSharedColumn.DataPropertyName = "IsShared";
            this.IsSharedColumn.HeaderText = "Shared";
            this.IsSharedColumn.Name = "IsSharedColumn";
            this.IsSharedColumn.ReadOnly = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(798, 140);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(105, 35);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 356);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_AddNewParameter);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "RoomForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parameters To Copy";
            this.Load += new System.EventHandler(this.RoomForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btn_AddNewParameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterStorageTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterIDColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSharedColumn;
        private System.Windows.Forms.Button btn_Delete;
    }
}