namespace EmptyPackageSummary
{
    partial class frmEmptyPackageSummary
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvAreas = new System.Windows.Forms.DataGridView();
            this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercentOfTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkColorCode = new System.Windows.Forms.CheckBox();
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).BeginInit();
            this.grpSummary.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(430, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(134, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "&Analyze Report...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.AllowDrop = true;
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(412, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Comma Separated Values|*.csv";
            // 
            // btnView
            // 
            this.btnView.Enabled = false;
            this.btnView.Location = new System.Drawing.Point(570, 9);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(100, 23);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "&View Report...";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(676, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "C&lear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvAreas
            // 
            this.dgvAreas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAreas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Area,
            this.Count,
            this.PercentOfTotal});
            this.dgvAreas.Location = new System.Drawing.Point(369, 48);
            this.dgvAreas.Name = "dgvAreas";
            this.dgvAreas.ReadOnly = true;
            this.dgvAreas.Size = new System.Drawing.Size(382, 537);
            this.dgvAreas.TabIndex = 7;
            // 
            // Area
            // 
            this.Area.HeaderText = "Area";
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.HeaderText = "Count";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            // 
            // PercentOfTotal
            // 
            this.PercentOfTotal.HeaderText = "Percent Of Total";
            this.PercentOfTotal.Name = "PercentOfTotal";
            this.PercentOfTotal.ReadOnly = true;
            this.PercentOfTotal.Width = 120;
            // 
            // chkColorCode
            // 
            this.chkColorCode.AutoSize = true;
            this.chkColorCode.Location = new System.Drawing.Point(6, 19);
            this.chkColorCode.Name = "chkColorCode";
            this.chkColorCode.Size = new System.Drawing.Size(114, 17);
            this.chkColorCode.TabIndex = 7;
            this.chkColorCode.Text = "Use Color Coding?";
            this.chkColorCode.UseVisualStyleBackColor = true;
            this.chkColorCode.CheckedChanged += new System.EventHandler(this.chkColorCode_CheckedChanged);
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.txtSummary);
            this.grpSummary.Location = new System.Drawing.Point(21, 79);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(342, 167);
            this.grpSummary.TabIndex = 5;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Summary";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(6, 19);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.Size = new System.Drawing.Size(330, 142);
            this.txtSummary.TabIndex = 12;
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkColorCode);
            this.grpOptions.Location = new System.Drawing.Point(21, 294);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(342, 100);
            this.grpOptions.TabIndex = 6;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(351, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // frmEmptyPackageSummary
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 597);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.dgvAreas);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtFilePath);
            this.Name = "frmEmptyPackageSummary";
            this.Text = "Empty Package Summary - Version 0.3.11";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmEmptyPackageSummary_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmEmptyPackageSummary_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).EndInit();
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvAreas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercentOfTotal;
        private System.Windows.Forms.CheckBox chkColorCode;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

