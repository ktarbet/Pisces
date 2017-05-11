﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Reclamation.TimeSeries.Usgs;
using Reclamation.Core;
using System.Diagnostics;

namespace Reclamation.TimeSeries.Forms.ImportForms
{
    public partial class ImportIdwrData : Form
    {
        public ImportIdwrData()
        {
            InitializeComponent();

            this.timeSelectorBeginEnd1.T1 = DateTime.Now.AddDays(-10);
            this.timeSelectorBeginEnd1.T2 = DateTime.Now.AddDays(-1);
        }


        public DateTime T2
        {
            get { return this.timeSelectorBeginEnd1.T2; }
        }

        public DateTime T1
        {
            get { return timeSelectorBeginEnd1.T1; }
        }


        private void linkLabelIdwrInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://idwr.idaho.gov/files/help/Accounting-Time-Series-Users-Manual.pdf");
        }


        private void comboBoxRiverSystems_OnDropDown(object sender, EventArgs e)
        {
            this.comboBoxRiverSystems.DataSource = null;
            this.comboBoxRiverSystems.SelectedValue = null;
            this.comboBoxRiverSystems.Items.Clear();
            var dTab = Reclamation.TimeSeries.IDWR.Utilities.GetIdwrRiverSystems();
            this.comboBoxRiverSystems.DataSource = dTab;
            this.comboBoxRiverSystems.ValueMember = "River";
            this.comboBoxRiverSystems.DisplayMember = "Name";

            ComboBox senderComboBox = this.comboBoxRiverSystems;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth, maxWidth = 156;
            for (int i = 0; i < dTab.Rows.Count; i++)
            {
                string s = dTab.Rows[i]["Name"].ToString();
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (maxWidth < newWidth)
                { maxWidth = newWidth; }
            }
            senderComboBox.DropDownWidth = maxWidth;
        }

        private void comboBoxRiverSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRiverSystems.SelectedValue is string)
            {
                this.comboBoxRiverSites.DataSource = null;
                this.comboBoxRiverSites.Items.Clear();
                var dTab = Reclamation.TimeSeries.IDWR.Utilities.GetIdwrRiverSites(this.comboBoxRiverSystems.SelectedValue.ToString());
                this.comboBoxRiverSites.DataSource = dTab;
                this.comboBoxRiverSites.ValueMember = "SiteID";
                this.comboBoxRiverSites.DisplayMember = "SiteName";

                ComboBox senderComboBox = this.comboBoxRiverSites;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth, maxWidth = 156;
                for (int i = 0; i < dTab.Rows.Count; i++)
                {
                    string s = dTab.Rows[i]["SiteName"].ToString();
                    newWidth = (int)g.MeasureString(s, font).Width
                        + vertScrollBarWidth;
                    if (maxWidth < newWidth)
                    { maxWidth = newWidth; }
                }
                senderComboBox.DropDownWidth = maxWidth;
            }
        }

        private void comboBoxRiverSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRiverSites.SelectedValue is string)
            {
                var dTab = Reclamation.TimeSeries.IDWR.Utilities.GetIdwrSiteInfo(this.comboBoxRiverSites.SelectedValue.ToString());

                switch (dTab.Rows[0]["SiteType"].ToString())
                {
                    case "F": case "Y": case "E": case "W":
                        {
                            this.radioButtonGH.Enabled = false;
                            this.radioButtonFB.Enabled = false;
                            this.radioButtonAF.Enabled = false;
                            this.radioButtonQD.Enabled = true;
                            this.radioButtonQD.Checked = true;
                            break;
                        }
                    case "D":
                        {
                            this.radioButtonGH.Enabled = true;
                            this.radioButtonFB.Enabled = false;
                            this.radioButtonAF.Enabled = false;
                            this.radioButtonQD.Enabled = true;
                            this.radioButtonQD.Checked = true;
                            break;
                        }
                    case "R":
                        {
                            this.radioButtonGH.Enabled = false;
                            this.radioButtonFB.Enabled = true;
                            this.radioButtonAF.Enabled = true;
                            this.radioButtonQD.Enabled = false;
                            this.radioButtonFB.Checked = true;
                            break;
                        }
                    default:
                        {
                            this.radioButtonGH.Enabled = false;
                            this.radioButtonFB.Enabled = false;
                            this.radioButtonAF.Enabled = false;
                            this.radioButtonQD.Enabled = false;
                            this.radioButtonFB.Checked = false;
                            break;
                        }
                }
                this.labelName.Text = "Name: " + dTab.Rows[0]["FullName"].ToString();
                this.labelSID.Text = "Site ID: " + dTab.Rows[0]["SiteID"].ToString();
                this.labelYears.Text = "Years Available: " + dTab.Rows[0]["Years"].ToString();
            }
        }
    }
}
