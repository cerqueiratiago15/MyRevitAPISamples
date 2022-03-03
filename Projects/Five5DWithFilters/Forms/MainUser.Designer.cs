namespace Five5DWithFilters
{
    partial class MainUser
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lboxParameters = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnRemoveParameter = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnSeeService = new System.Windows.Forms.Button();
            this.btnFilters = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSaveService = new System.Windows.Forms.Button();
            this.btnResetControls = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.elementHost1);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(914, 671);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtered Elements";
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 18);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(908, 650);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.elementHost1_ChildChanged);
            this.elementHost1.Child = null;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnAddParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParameter.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddParameter.Location = new System.Drawing.Point(79, 653);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(151, 42);
            this.btnAddParameter.TabIndex = 1;
            this.btnAddParameter.Text = "Adicionar Parâmetro";
            this.btnAddParameter.UseVisualStyleBackColor = false;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.txtFileName);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.btnRemoveParameter);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnAddParameter);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.btnSeeService);
            this.groupBox2.Controls.Add(this.btnFilters);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(932, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 701);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(492, 22);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(6, 11);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(492, 22);
            this.txtFileName.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.lboxParameters);
            this.panel1.Location = new System.Drawing.Point(6, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 287);
            this.panel1.TabIndex = 3;
            // 
            // lboxParameters
            // 
            this.lboxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxParameters.FormattingEnabled = true;
            this.lboxParameters.ItemHeight = 16;
            this.lboxParameters.Location = new System.Drawing.Point(3, 6);
            this.lboxParameters.Name = "lboxParameters";
            this.lboxParameters.Size = new System.Drawing.Size(633, 276);
            this.lboxParameters.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button4.Location = new System.Drawing.Point(420, 653);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 42);
            this.button4.TabIndex = 1;
            this.button4.Text = "Remover Parâmetro";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnRemoveParameter_Click);
            // 
            // btnRemoveParameter
            // 
            this.btnRemoveParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveParameter.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnRemoveParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveParameter.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRemoveParameter.Location = new System.Drawing.Point(420, 653);
            this.btnRemoveParameter.Name = "btnRemoveParameter";
            this.btnRemoveParameter.Size = new System.Drawing.Size(151, 42);
            this.btnRemoveParameter.TabIndex = 1;
            this.btnRemoveParameter.Text = "Remover Parâmetro";
            this.btnRemoveParameter.UseVisualStyleBackColor = false;
            this.btnRemoveParameter.Click += new System.EventHandler(this.btnRemoveParameter_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(79, 653);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Adicionar Parâmetro";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnAddParameter_Click_1);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button7.FlatAppearance.BorderSize = 2;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button7.Location = new System.Drawing.Point(504, 54);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(154, 42);
            this.button7.TabIndex = 2;
            this.button7.Text = "Selecionar serviço";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnSeeService
            // 
            this.btnSeeService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeeService.BackColor = System.Drawing.Color.White;
            this.btnSeeService.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSeeService.FlatAppearance.BorderSize = 2;
            this.btnSeeService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeeService.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSeeService.Location = new System.Drawing.Point(79, 236);
            this.btnSeeService.Name = "btnSeeService";
            this.btnSeeService.Size = new System.Drawing.Size(492, 42);
            this.btnSeeService.TabIndex = 2;
            this.btnSeeService.Text = "Visualizar Serviços";
            this.btnSeeService.UseVisualStyleBackColor = false;
            // 
            // btnFilters
            // 
            this.btnFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilters.BackColor = System.Drawing.Color.White;
            this.btnFilters.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilters.FlatAppearance.BorderSize = 2;
            this.btnFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilters.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFilters.Location = new System.Drawing.Point(79, 300);
            this.btnFilters.Name = "btnFilters";
            this.btnFilters.Size = new System.Drawing.Size(492, 42);
            this.btnFilters.TabIndex = 2;
            this.btnFilters.Text = "Configurar Filtros de seleção";
            this.btnFilters.UseVisualStyleBackColor = false;
            this.btnFilters.Click += new System.EventHandler(this.btnFilters_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(504, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 42);
            this.button2.TabIndex = 2;
            this.button2.Text = "Planilha de Serviços";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSaveService
            // 
            this.btnSaveService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveService.BackColor = System.Drawing.Color.White;
            this.btnSaveService.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSaveService.FlatAppearance.BorderSize = 2;
            this.btnSaveService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveService.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveService.Location = new System.Drawing.Point(290, 685);
            this.btnSaveService.Name = "btnSaveService";
            this.btnSaveService.Size = new System.Drawing.Size(151, 42);
            this.btnSaveService.TabIndex = 1;
            this.btnSaveService.Text = "Save";
            this.btnSaveService.UseVisualStyleBackColor = false;
            this.btnSaveService.Click += new System.EventHandler(this.btnSaveService_Click);
            // 
            // btnResetControls
            // 
            this.btnResetControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetControls.BackColor = System.Drawing.Color.White;
            this.btnResetControls.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnResetControls.FlatAppearance.BorderSize = 2;
            this.btnResetControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetControls.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnResetControls.Location = new System.Drawing.Point(51, 685);
            this.btnResetControls.Name = "btnResetControls";
            this.btnResetControls.Size = new System.Drawing.Size(151, 42);
            this.btnResetControls.TabIndex = 1;
            this.btnResetControls.Text = "Reset";
            this.btnResetControls.UseVisualStyleBackColor = false;
            this.btnResetControls.Click += new System.EventHandler(this.btnResetControls_Click);
            // 
            // MainUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1608, 739);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnResetControls);
            this.Controls.Add(this.btnSaveService);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orçando com Filtros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUser_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnFilters;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnRemoveParameter;
        private System.Windows.Forms.Button btnSaveService;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lboxParameters;
        private System.Windows.Forms.Button btnSeeService;
        private System.Windows.Forms.Button btnResetControls;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
    }
}