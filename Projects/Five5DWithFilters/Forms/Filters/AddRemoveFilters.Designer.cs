namespace Five5DWithFilters
{
    partial class AddRemoveFilters
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
            this.lboxAppliedFilters = new System.Windows.Forms.ListBox();
            this.btnRmvFilter = new System.Windows.Forms.Button();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lboxAppliedFilters
            // 
            this.lboxAppliedFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxAppliedFilters.FormattingEnabled = true;
            this.lboxAppliedFilters.ItemHeight = 16;
            this.lboxAppliedFilters.Location = new System.Drawing.Point(12, 60);
            this.lboxAppliedFilters.Name = "lboxAppliedFilters";
            this.lboxAppliedFilters.Size = new System.Drawing.Size(333, 372);
            this.lboxAppliedFilters.TabIndex = 0;
            // 
            // btnRmvFilter
            // 
            this.btnRmvFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRmvFilter.Location = new System.Drawing.Point(351, 60);
            this.btnRmvFilter.Name = "btnRmvFilter";
            this.btnRmvFilter.Size = new System.Drawing.Size(125, 42);
            this.btnRmvFilter.TabIndex = 1;
            this.btnRmvFilter.Text = "Remover";
            this.btnRmvFilter.UseVisualStyleBackColor = true;
            this.btnRmvFilter.Click += new System.EventHandler(this.btnRmvFilter_Click);
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNew.Location = new System.Drawing.Point(351, 114);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(125, 42);
            this.btnCreateNew.TabIndex = 1;
            this.btnCreateNew.Text = "Adicionar";
            this.btnCreateNew.UseVisualStyleBackColor = true;
            this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(333, 51);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Os filtros aplicados terão lógicas independetes a vista auxiliar mostrará os elem" +
    "entos aplicados a cada um dos mesmos ";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(351, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 42);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // AddRemoveFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 455);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCreateNew);
            this.Controls.Add(this.btnRmvFilter);
            this.Controls.Add(this.lboxAppliedFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRemoveFilters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtros Aplicados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxAppliedFilters;
        private System.Windows.Forms.Button btnRmvFilter;
        private System.Windows.Forms.Button btnCreateNew;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnOK;
    }
}