﻿namespace Reclamation.TimeSeries.Forms.Hydromet
{
    partial class ServerSelection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCustomSource = new System.Windows.Forms.RadioButton();
            this.textBoxCustomSource = new System.Windows.Forms.TextBox();
            this.radioButtonYakLinux = new System.Windows.Forms.RadioButton();
            this.textBoxDbName = new System.Windows.Forms.TextBox();
            this.labelDbName = new System.Windows.Forms.Label();
            this.radioButtonPnHydromet = new System.Windows.Forms.RadioButton();
            this.radioButtonYakHydromet = new System.Windows.Forms.RadioButton();
            this.radioButtonBoiseLinux = new System.Windows.Forms.RadioButton();
            this.radioButtonGP = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCustomSource);
            this.groupBox1.Controls.Add(this.textBoxCustomSource);
            this.groupBox1.Controls.Add(this.radioButtonYakLinux);
            this.groupBox1.Controls.Add(this.textBoxDbName);
            this.groupBox1.Controls.Add(this.labelDbName);
            this.groupBox1.Controls.Add(this.radioButtonPnHydromet);
            this.groupBox1.Controls.Add(this.radioButtonYakHydromet);
            this.groupBox1.Controls.Add(this.radioButtonBoiseLinux);
            this.groupBox1.Controls.Add(this.radioButtonGP);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(364, 226);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "time series data source";
            // 
            // radioButtonCustomSource
            // 
            this.radioButtonCustomSource.AutoSize = true;
            this.radioButtonCustomSource.Location = new System.Drawing.Point(15, 118);
            this.radioButtonCustomSource.Name = "radioButtonCustomSource";
            this.radioButtonCustomSource.Size = new System.Drawing.Size(97, 17);
            this.radioButtonCustomSource.TabIndex = 50;
            this.radioButtonCustomSource.TabStop = true;
            this.radioButtonCustomSource.Text = "Custom Source";
            this.radioButtonCustomSource.UseVisualStyleBackColor = true;
            // 
            // textBoxCustomSource
            // 
            this.textBoxCustomSource.Location = new System.Drawing.Point(118, 118);
            this.textBoxCustomSource.Name = "textBoxCustomSource";
            this.textBoxCustomSource.Size = new System.Drawing.Size(110, 20);
            this.textBoxCustomSource.TabIndex = 48;
            this.textBoxCustomSource.TextChanged += new System.EventHandler(this.textBoxCustomSource_TextChanged);
            // 
            // radioButtonYakLinux
            // 
            this.radioButtonYakLinux.Location = new System.Drawing.Point(15, 93);
            this.radioButtonYakLinux.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonYakLinux.Name = "radioButtonYakLinux";
            this.radioButtonYakLinux.Size = new System.Drawing.Size(136, 20);
            this.radioButtonYakLinux.TabIndex = 47;
            this.radioButtonYakLinux.Text = "Yakima Linux";
            this.radioButtonYakLinux.CheckedChanged += new System.EventHandler(this.serverChanged);
            // 
            // textBoxDbName
            // 
            this.textBoxDbName.Location = new System.Drawing.Point(35, 182);
            this.textBoxDbName.Name = "textBoxDbName";
            this.textBoxDbName.Size = new System.Drawing.Size(186, 20);
            this.textBoxDbName.TabIndex = 46;
            this.textBoxDbName.Text = "timeseries";
            // 
            // labelDbName
            // 
            this.labelDbName.AutoSize = true;
            this.labelDbName.Location = new System.Drawing.Point(32, 165);
            this.labelDbName.Name = "labelDbName";
            this.labelDbName.Size = new System.Drawing.Size(83, 13);
            this.labelDbName.TabIndex = 45;
            this.labelDbName.Text = "database name:";
            // 
            // radioButtonPnHydromet
            // 
            this.radioButtonPnHydromet.Location = new System.Drawing.Point(208, 23);
            this.radioButtonPnHydromet.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonPnHydromet.Name = "radioButtonPnHydromet";
            this.radioButtonPnHydromet.Size = new System.Drawing.Size(152, 20);
            this.radioButtonPnHydromet.TabIndex = 0;
            this.radioButtonPnHydromet.Text = "Boise Hydromet VMS";
            this.radioButtonPnHydromet.Visible = false;
            this.radioButtonPnHydromet.CheckedChanged += new System.EventHandler(this.serverChanged);
            // 
            // radioButtonYakHydromet
            // 
            this.radioButtonYakHydromet.Location = new System.Drawing.Point(15, 45);
            this.radioButtonYakHydromet.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonYakHydromet.Name = "radioButtonYakHydromet";
            this.radioButtonYakHydromet.Size = new System.Drawing.Size(112, 20);
            this.radioButtonYakHydromet.TabIndex = 3;
            this.radioButtonYakHydromet.Text = "Yakima Hydromet";
            this.radioButtonYakHydromet.CheckedChanged += new System.EventHandler(this.serverChanged);
            // 
            // radioButtonBoiseLinux
            // 
            this.radioButtonBoiseLinux.Checked = true;
            this.radioButtonBoiseLinux.Location = new System.Drawing.Point(15, 23);
            this.radioButtonBoiseLinux.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoiseLinux.Name = "radioButtonBoiseLinux";
            this.radioButtonBoiseLinux.Size = new System.Drawing.Size(136, 20);
            this.radioButtonBoiseLinux.TabIndex = 5;
            this.radioButtonBoiseLinux.TabStop = true;
            this.radioButtonBoiseLinux.Text = "Boise Hydromet ";
            this.radioButtonBoiseLinux.CheckedChanged += new System.EventHandler(this.serverChanged);
            // 
            // radioButtonGP
            // 
            this.radioButtonGP.Location = new System.Drawing.Point(15, 69);
            this.radioButtonGP.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonGP.Name = "radioButtonGP";
            this.radioButtonGP.Size = new System.Drawing.Size(112, 20);
            this.radioButtonGP.TabIndex = 4;
            this.radioButtonGP.Text = "Billings Hydromet";
            this.radioButtonGP.CheckedChanged += new System.EventHandler(this.serverChanged);
            // 
            // ServerSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ServerSelection";
            this.Size = new System.Drawing.Size(364, 226);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonBoiseLinux;
        private System.Windows.Forms.RadioButton radioButtonGP;
        private System.Windows.Forms.RadioButton radioButtonYakHydromet;
        private System.Windows.Forms.RadioButton radioButtonPnHydromet;
        private System.Windows.Forms.TextBox textBoxDbName;
        private System.Windows.Forms.Label labelDbName;
        private System.Windows.Forms.RadioButton radioButtonYakLinux;
        private System.Windows.Forms.TextBox textBoxCustomSource;
        private System.Windows.Forms.RadioButton radioButtonCustomSource;
    }
}
